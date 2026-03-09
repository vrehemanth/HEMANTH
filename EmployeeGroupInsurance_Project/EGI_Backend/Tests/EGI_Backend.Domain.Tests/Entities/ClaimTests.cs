using System;
using System.Linq;
using Xunit;
using EGI_Backend.Domain.Entities;
using EGI_Backend.Domain.Enums;

namespace EGI_Backend.Domain.Tests.Entities
{
    public class ClaimTests
    {
        [Fact]
        public void Claim_DefaultInitialization_SetsPendingStatus()
        {
            var claim = new Claim();
            Assert.Equal(ClaimStatus.Pending, claim.Status);
            Assert.NotNull(claim.Documents);
            Assert.Empty(claim.Documents);
        }

        [Fact]
        public void Claim_SetProperties_ValuesAssignedCorrectly()
        {
            var id = Guid.NewGuid();
            var memberId = Guid.NewGuid();
            var claim = new Claim
            {
                Id = id,
                MemberId = memberId,
                ClaimAmount = 500.00m,
                ClaimReason = "Flu checkup"
            };

            Assert.Equal(id, claim.Id);
            Assert.Equal(memberId, claim.MemberId);
            Assert.Equal(500.00m, claim.ClaimAmount);
            Assert.Equal("Flu checkup", claim.ClaimReason);
        }

        [Fact]
        public void Claim_UpdateStatus_StatusChangedToApproved()
        {
            var claim = new Claim { Status = ClaimStatus.Pending };
            claim.Status = ClaimStatus.Approved;
            Assert.Equal(ClaimStatus.Approved, claim.Status);
        }

        [Fact]
        public void Claim_AddDocument_AddsToCollection()
        {
            var claim = new Claim();
            var doc = new ClaimDocument { Id = Guid.NewGuid(), FilePath = "http://example.com/doc.pdf" };
            claim.Documents.Add(doc);
            
            Assert.Single(claim.Documents);
            Assert.Equal("http://example.com/doc.pdf", claim.Documents.First().FilePath);
        }

        [Fact]
        public void Claim_AssignReviewer_UpdatesReviewerProperties()
        {
            var reviewerId = Guid.NewGuid();
            var claim = new Claim();
            
            claim.ReviewedBy = reviewerId;
            var time = DateTime.UtcNow;
            claim.ReviewedAt = time;

            Assert.Equal(reviewerId, claim.ReviewedBy);
            Assert.Equal(time, claim.ReviewedAt);
        }
    }
}
