﻿#region BSD License
/* 
Copyright (c) 2012, Clarius Consulting
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

namespace Clide.Composition
{
    using System;
    using System.ComponentModel.Composition.Primitives;

    internal class LocalDecoratingCatalog : DecoratingReflectionCatalog
    {
        public LocalDecoratingCatalog(Guid hostId, ComposablePartCatalog catalogToDecorate)
            : base(catalogToDecorate)
        {
            this.ExportDecorator = context =>
                !IsClideExport(context.ExportDefinition) ? null :
                new ExportInfo(ContractNames.AsLocal(hostId, context.ExportDefinition.ContractName));

            this.ImportDecorator = context =>
                !IsClideImport(context.ImportDefinition) ? null :
                new ImportInfo(ContractNames.AsLocal(hostId, context.ImportDefinition.ContractName));
        }

        private static bool IsClideExport(ExportDefinition export)
        {
            return export.ContractName.StartsWith("Clide.");
        }

        private static bool IsClideImport(ImportDefinition import)
        {
            return import.ContractName.StartsWith("Clide.");
        }
    }
}