﻿// -----------------------------------------------------------------------------
﻿//
﻿// Copyright Microsoft Corporation
﻿// Licensed under the Apache License, Version 2.0 (the "License");
﻿// you may not use this file except in compliance with the License.
﻿// You may obtain a copy of the License at
﻿// http://www.apache.org/licenses/LICENSE-2.0
﻿// Unless required by applicable law or agreed to in writing, software
﻿// distributed under the License is distributed on an "AS IS" BASIS,
﻿// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
﻿// See the License for the specific language governing permissions and
﻿// limitations under the License.
﻿// -----------------------------------------------------------------------------
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.Azure.Commands.Batch.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.Azure.Batch;
    
    
    public partial class PSJobPreparationTaskExecutionInformation
    {
        
        internal Microsoft.Azure.Batch.JobPreparationTaskExecutionInformation omObject;
        
        private PSTaskSchedulingError schedulingError;
        
        internal PSJobPreparationTaskExecutionInformation(Microsoft.Azure.Batch.JobPreparationTaskExecutionInformation omObject)
        {
            if ((omObject == null))
            {
                throw new System.ArgumentNullException("omObject");
            }
            this.omObject = omObject;
        }
        
        public System.DateTime? EndTime
        {
            get
            {
                return this.omObject.EndTime;
            }
        }
        
        public System.Int32? ExitCode
        {
            get
            {
                return this.omObject.ExitCode;
            }
        }
        
        public System.DateTime? LastRetryTime
        {
            get
            {
                return this.omObject.LastRetryTime;
            }
        }
        
        public int RetryCount
        {
            get
            {
                return this.omObject.RetryCount;
            }
        }
        
        public PSTaskSchedulingError SchedulingError
        {
            get
            {
                if (((this.schedulingError == null) 
                            && (this.omObject.SchedulingError != null)))
                {
                    this.schedulingError = new PSTaskSchedulingError(this.omObject.SchedulingError);
                }
                return this.schedulingError;
            }
        }
        
        public System.DateTime StartTime
        {
            get
            {
                return this.omObject.StartTime;
            }
        }
        
        public Microsoft.Azure.Batch.Common.JobPreparationTaskState State
        {
            get
            {
                return this.omObject.State;
            }
        }
        
        public string TaskRootDirectory
        {
            get
            {
                return this.omObject.TaskRootDirectory;
            }
        }
        
        public string TaskRootDirectoryUrl
        {
            get
            {
                return this.omObject.TaskRootDirectoryUrl;
            }
        }
    }
}
