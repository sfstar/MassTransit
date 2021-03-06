﻿// Copyright 2007-2015 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit
{
    using System;
    using System.Threading.Tasks;
    using PipeConfigurators;


    public static class DelegatePipeConfiguratorExtensions
    {
        /// <summary>
        /// Executes a synchronous method on the pipe
        /// </summary>
        /// <typeparam name="T">The context type</typeparam>
        /// <param name="configurator">The pipe configurator</param>
        /// <param name="callback">The callback to invoke</param>
        public static void UseExecute<T>(this IPipeConfigurator<T> configurator, Action<T> callback)
            where T : class, PipeContext
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var pipeBuilderConfigurator = new DelegatePipeSpecification<T>(callback);

            configurator.AddPipeSpecification(pipeBuilderConfigurator);
        }

        /// <summary>
        /// Executes an asynchronous method on the pipe
        /// </summary>
        /// <typeparam name="T">The context type</typeparam>
        /// <param name="configurator">The pipe configurator</param>
        /// <param name="callback">The callback to invoke</param>
        public static void UseExecuteAsync<T>(this IPipeConfigurator<T> configurator, Func<T, Task> callback)
            where T : class, PipeContext
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var pipeBuilderConfigurator = new AsyncDelegatePipeSpecification<T>(callback);

            configurator.AddPipeSpecification(pipeBuilderConfigurator);
        }

        /// <summary>
        /// Adds a callback filter to the send pipeline
        /// </summary>
        /// <param name="configurator"></param>
        /// <param name="callback">The callback to invoke</param>
        public static void UseSendExecute(this ISendPipeConfigurator configurator, Action<SendContext> callback)
        {
            var specification = new DelegatePipeSpecification<SendContext>(callback);

            configurator.AddPipeSpecification(specification);
        }

        /// <summary>
        /// Adds a callback filter to the send pipeline
        /// </summary>
        /// <param name="configurator"></param>
        /// <param name="callback">The callback to invoke</param>
        public static void UseSendExecuteAsync(this ISendPipeConfigurator configurator, Func<SendContext, Task> callback)
        {
            var specification = new AsyncDelegatePipeSpecification<SendContext>(callback);

            configurator.AddPipeSpecification(specification);
        }

        /// <summary>
        /// Adds a callback filter to the send pipeline
        /// </summary>
        /// <param name="configurator"></param>
        /// <param name="callback">The callback to invoke</param>
        public static void UseSendExecute<T>(this ISendPipeConfigurator configurator, Action<SendContext<T>> callback)
            where T : class
        {
            var specification = new DelegatePipeSpecification<SendContext<T>>(callback);

            configurator.AddPipeSpecification(specification);
        }

        /// <summary>
        /// Adds a callback filter to the send pipeline
        /// </summary>
        /// <param name="configurator"></param>
        /// <param name="callback">The callback to invoke</param>
        public static void UseSendExecuteAsync<T>(this ISendPipeConfigurator configurator, Func<SendContext<T>, Task> callback)
            where T : class
        {
            var specification = new AsyncDelegatePipeSpecification<SendContext<T>>(callback);

            configurator.AddPipeSpecification(specification);
        }
    }
}