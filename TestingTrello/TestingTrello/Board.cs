
namespace TestingTrello
{
    public class Board
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Prefs Prefs { get; set; }
        public Root Root { get; set; }
        public LabelNames LabelNames { get; set; }
    }

    //public enum Colors
    //{
    //    red = ffb04632

    //}

    public class Prefs
    {
        public string PermissionLevel { get; set; }
        public bool HideVotes { get; set; }
        public string Voting { get; set; }
        public string Comments { get; set; }
        public string Invitations { get; set; }
        public bool SelfJoin { get; set; }
        public bool CardCovers { get; set; }
        public bool IsTemplate { get; set; }
        public string CardAging { get; set; }
        public bool CalendarFeedEnabled { get; set; }
        public string Background { get; set; }
        public object BackgroundImage { get; set; }
        public object BackgroundImageScaled { get; set; }
        public bool BackgroundTile { get; set; }
        public string BackgroundBrightness { get; set; }
        public string BackgroundColor { get; set; }
        public string BackgroundBottomColor { get; set; }
        public string BackgroundTopColor { get; set; }
        public bool CanBePublic { get; set; }
        public bool CanBeEnterprise { get; set; }
        public bool CanBeOrg { get; set; }
        public bool CanBePrivate { get; set; }
        public bool CanInvite { get; set; }
    }


    public class LabelNames
    {
        public string Green { get; set; }
        public string Yellow { get; set; }
        public string Orange { get; set; }
        public string Red { get; set; }
        public string Purple { get; set; }
        public string Blue { get; set; }
        public string Sky { get; set; }
        public string Lime { get; set; }
        public string Pink { get; set; }
        public string Black { get; set; }
    }

    public class Root
    {
        public string Desc { get; set; }
        public object DescData { get; set; }
        public bool Closed { get; set; }
        public string IdOrganization { get; set; }
        public object IdEnterprise { get; set; }
        public bool Pinned { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
    }
}



