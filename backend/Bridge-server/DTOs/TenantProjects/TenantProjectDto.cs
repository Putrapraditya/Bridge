﻿namespace Bridge_server.DTOs.TenantProjects
{
    public class TenantProjectDto
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
