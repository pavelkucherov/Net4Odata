using System.Web.Http;

using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using courses_odata.Model;
/*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using courses_odata.Model;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<TeachingActivity>("TeachingActivities");
    builder.EntitySet<Lecture>("Lectures"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

namespace net4odata
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Web API configuration and services
            ODataModelBuilder builder = new ODataConventionModelBuilder();

            //builder.EntitySet<TeachingActivity>("TeachingActivities");
            //builder.EntitySet<Lecture>("Lectures");

            builder.EntitySet<Course>(nameof(Course))
                      .EntityType
                      .Filter() // Allow for the $filter Command
                      .Count() // Allow for the $count Command
                      .Expand() // Allow for the $expand Command
                      .OrderBy() // Allow for the $orderby Command
                      .Page() // Allow for the $top and $skip Commands
                      .Select() // Allow for the $select Command
                      .ContainsMany(x => x.Lectures);

            builder.EntitySet<Lecture>(nameof(Lecture))
                            .EntityType
                            .Filter() // Allow for the $filter Command
                            .Count() // Allow for the $count Command
                            .Expand() // Allow for the $expand Command
                            .OrderBy() // Allow for the $orderby Command
                            .Page() // Allow for the $top and $skip Commands
                            .Select() // Allow for the $select Command
                            .ContainsMany(x => x.TeachingActivities);

            builder.EntitySet<TeachingActivity>("TeachingActivities"/*nameof(TeachingActivity)*/)
                              .EntityType
                              .Abstract()
                              .Filter() // Allow for the $filter Command
                              .Count() // Allow for the $count Command
                              .Expand() // Allow for the $expand Command
                              .OrderBy() // Allow for the $orderby Command
                              .Page() // Allow for the $top and $skip Commands
                              .Select(); // Allow for the $select Command

            builder.EntitySet<Answer>(nameof(Answer))
                              .EntityType
                              .Filter() // Allow for the $filter Command
                              .Count() // Allow for the $count Command
                              .Expand() // Allow for the $expand Command
                              .OrderBy() // Allow for the $orderby Command
                              .Page() // Allow for the $top and $skip Commands
                              .Select(); // Allow for the $select Command

            builder.EntitySet<MultipleChoice>(nameof(MultipleChoice))
                              .EntityType
                              .DerivesFrom<TeachingActivity>()
                              .Filter() // Allow for the $filter Command
                              .Count() // Allow for the $count Command
                              .Expand() // Allow for the $expand Command
                              .OrderBy() // Allow for the $orderby Command
                              .Page() // Allow for the $top and $skip Commands
                              .Select() // Allow for the $select Command

                              .ContainsMany(x => x.Answers)
                      .AutomaticallyExpand(false);

            builder.EntitySet<Slide>(nameof(Slide))
                              .EntityType
                              .DerivesFrom<TeachingActivity>()
                              .Filter() // Allow for the $filter Command
                              .Count() // Allow for the $count Command
                              .Expand() // Allow for the $expand Command
                              .OrderBy() // Allow for the $orderby Command
                              .Page() // Allow for the $top and $skip Commands
                              .Select() // Allow for the $select Command
                            ;//  .ContainsMany(x => x.Answers2)
                             //  .AutomaticallyExpand(false);






            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "OData",
                model: builder.GetEdmModel());

            
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }
    }
}
