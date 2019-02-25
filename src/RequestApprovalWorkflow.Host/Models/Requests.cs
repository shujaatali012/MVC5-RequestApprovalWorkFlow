using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RequestApprovalWorkflow.Host.Models
{
    public class Requests
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public int RequstStatusId { get; set; }

        public virtual RequestStatus Status { get; set; }

        public DateTime StatusDate { get; set; }
    }

    public class RequestStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class UserRequests
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        public int RequestId { get; set; }

        public virtual Requests Request { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class RequestViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public int RequstStatusId { get; set; }

        public string RequstStatus { get; set; }

        public DateTime StatusDate { get; set; }

        public string UserId { get; set; }

        public int DepartmentId { get; set; }

        public int DesignationId { get; set; }

        public int LoggedInUserDesignationId { get; set; }
    }
}