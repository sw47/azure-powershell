﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Commands.RecoveryServices.Backup.Cmdlets.Models;
using Microsoft.Azure.Commands.RecoveryServices.Backup.Cmdlets.ProviderModel;
using Microsoft.Azure.Commands.RecoveryServices.Backup.Properties;
using Microsoft.Azure.Commands.RecoveryServices.Backup.Helpers;
using HydraModel = Microsoft.Azure.Management.RecoveryServices.Backup.Models;


namespace Microsoft.Azure.Commands.RecoveryServices.Backup.Cmdlets
{
    /// <summary>
    /// Enable Azure Backup protection
    /// </summary>
    [Cmdlet(VerbsLifecycle.Disable, "AzureRmRecoveryServicesProtection"), OutputType(typeof(AzureRmRecoveryServicesJobBase))]
    public class DisableAzureRmRecoveryServicesProtection : RecoveryServicesBackupCmdletBase
    {
        [Parameter(Mandatory = true, HelpMessage = ParamHelpMsg.Item.ProtectedItem, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public AzureRmRecoveryServicesItemBase Item { get; set; }

        [Parameter(Position = 1, Mandatory = false, HelpMessage = ParamHelpMsg.Item.RemoveProtectionOption)]
        public SwitchParameter RemoveRecoveryPoints
        {
            get { return DeleteBackupData; }
            set { DeleteBackupData = value; }
        }

        [Parameter(Mandatory = false, HelpMessage = "Don't ask for confirmation.")]
        public SwitchParameter Force { get; set; }

        private bool DeleteBackupData;

        public override void ExecuteCmdlet()
        {
            ConfirmAction(
                Force.IsPresent,
                string.Format(Resources.DisableProtectionWarning, Item.Name),
                Resources.DisableProtectionMessage,
                Item.Name, () =>
                {
                    ExecutionBlock(() =>
                    {
                        base.ExecuteCmdlet();
                        PsBackupProviderManager providerManager = new PsBackupProviderManager(new Dictionary<System.Enum, object>()
                        {
                            {ItemParams.Item, Item},
                            {ItemParams.DeleteBackupData, this.DeleteBackupData},
                        }, HydraAdapter);

                        IPsBackupProvider psBackupProvider = providerManager.GetProviderInstance(Item.WorkloadType, Item.BackupManagementType);

                        var itemResponse = psBackupProvider.DisableProtection();

                        // Track Response and display job details

                        WriteDebug(Resources.TrackingOperationStatusURLForCompletion +
                                        itemResponse.AzureAsyncOperation);

                        var response = WaitForOperationCompletionUsingStatusLink(
                                                        itemResponse.AzureAsyncOperation,
                                                        HydraAdapter.GetProtectedItemOperationStatusByURL);

                        WriteDebug(Resources.FinalOperationStatus + response.OperationStatus.Status);

                        if (response.OperationStatus.Properties != null &&
                               ((HydraModel.OperationStatusJobExtendedInfo)response.OperationStatus.Properties).JobId != null)
                        {
                            var jobStatusResponse = (HydraModel.OperationStatusJobExtendedInfo)response.OperationStatus.Properties;
                            WriteObject(GetJobObject(jobStatusResponse.JobId));
                        }

                        if (response.OperationStatus.Status == HydraModel.OperationStatusValues.Failed)
                        {
                            var errorMessage = string.Format(Resources.DisableProtectionOperationFailed,
                            response.OperationStatus.OperationStatusError.Code,
                            response.OperationStatus.OperationStatusError.Message);
                            throw new Exception(errorMessage);
                        }
                    });
                });

        }
    }
}
