using System;

namespace SkillslabAssignment.Common.Interface
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}
