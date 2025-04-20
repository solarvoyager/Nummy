﻿namespace NummyApi.Entitites.Generic;

public class Auditable
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    // remove unused auditable properties
    //public Guid? CreatedById { get; set; }
    //public Guid? ModifiedBy { get; set; }
    //public Guid? DeletedBy { get; set; }
    //public DateTimeOffset? ModifiedAt { get; set; }
    //public DateTimeOffset? DeletedAt { get; set; }
    //public bool IsDeleted { get; set; }
}