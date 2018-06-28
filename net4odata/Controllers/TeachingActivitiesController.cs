using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using courses_odata.Model;
using net4odata.Models;
using System.Data.Entity;

namespace net4odata.Controllers
{
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

    //[Produces("application/json")]
    public class TeachingActivitiesController : ODataController
    {
        private net4odataContext db = new net4odataContext();

        // GET: odata/TeachingActivities
        [EnableQuery]
        public IQueryable<TeachingActivity> GetTeachingActivities()
        {
            return db.TeachingActivities;
        }

        // GET: odata/TeachingActivities(5)
        [EnableQuery]
        public SingleResult<TeachingActivity> GetTeachingActivity([FromODataUri] int key)
        {
            return SingleResult.Create(db.TeachingActivities.Where(teachingActivity => teachingActivity.Id == key));
        }

        

        // GET: odata/TeachingActivities(5)/Lecture
        [EnableQuery]
        public SingleResult<Lecture> GetLecture([FromODataUri] int key)
        {
            return SingleResult.Create(db.TeachingActivities.Where(m => m.Id == key).Select(m => m.Lecture));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeachingActivityExists(int key)
        {
            return db.TeachingActivities.Count(e => e.Id == key) > 0;
        }
    }
}
