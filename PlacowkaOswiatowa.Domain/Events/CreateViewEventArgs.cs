using System;

namespace PlacowkaOswiatowa.Domain.Models.Events
{
    public class CreateViewEventArgs : EventArgs
    {
        public Type WorkspaceTypeToCreate { get; set; }
        public CreateViewEventArgs(Type workspaceType)
        {
            WorkspaceTypeToCreate = workspaceType;
        }
    }
}
