namespace Studio
{
    using Nancy;
    using Nancy.Extensions;

    public class TalkbackModule : NancyModule
    {
        public TalkbackModule(ITalkbackService service)
        {
            Post["/talkback"] = _ =>
            {
                service.SetText(Request.Body.AsString());
                return Response.AsJson(new { Message = "OK" });
            };
        }
    }
}