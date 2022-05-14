using System;
using System.Collections.Generic;

#nullable disable

namespace prjTravelAlbumSys.Models
{
    public partial class TComment
    {
        public int FCommentId { get; set; }
        public int? FAlbumId { get; set; }
        public string FUid { get; set; }
        public string FName { get; set; }
        public string FComment { get; set; }
        public DateTime? FReleaseTime { get; set; }
    }
}
