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

using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Commands.Insights.OutputClasses;

namespace Microsoft.Azure.Commands.Insights.Events
{
    /// <summary>
    /// Get the list of events for at a ResourceGroup level.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureResourceGroupLog"), OutputType(typeof(List<IPSEventData>))]
    public class GetAzureResourceGroupLogCommand : EventCmdletBase
    {
        /// <summary>
        /// Gets or sets the resourcegroup parameters of this cmdlet
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = ResourceGroupName, Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "The resource group name")]
        [ValidateNotNullOrEmpty]
        public string ResourceGroup { get; set; }

        /// <summary>
        /// Process the parameters defined by this class  (a.k.a. particular parameters)
        /// </summary>
        /// <param name="currentQueryFilter">The current query filter</param>
        /// <returns>The query filter with the conditions for particular parameters added</returns>
        protected override string ProcessParticularParameters(string currentQueryFilter)
        {
            return this.AddConditionIfPResent(currentQueryFilter, "resourceGroupName", this.ResourceGroup);
        }
    }
}