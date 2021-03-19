using System.Threading.Tasks;
using Homework_4.Singleton_Transient_Scoped.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4.Singleton_Transient_Scoped.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RandomNumberController : ControllerBase
    {
        private readonly IScopedService _scopedService;
        private readonly IScopedService _scopedService2;
        private readonly ISingletonService _singletonService;
        private readonly ITransientService _transientService;
        private readonly ITransientService _transientService2;

        public RandomNumberController(IScopedService scopedService, ISingletonService singletonService,
            ITransientService transientService, IScopedService scopedService2, ITransientService transientService2)
        {
            _scopedService = scopedService;
            _singletonService = singletonService;
            _transientService = transientService;
            _scopedService2 = scopedService2;
            _transientService2 = transientService2;
        }

        [HttpGet]
        public IActionResult GetRandomNumber()
        {
            var scopedServiceResult = _scopedService.GetRandomNumber();
            var scopedServiceResult2 = _scopedService2.GetRandomNumber();
            var singletonServiceResult = _singletonService.GetRandomNumber();
            var transientServiceResult = _transientService.GetRandomNumber();
            var transientServiceResult2 = _transientService2.GetRandomNumber();
            var result =
                $"Random Singleton Number:{singletonServiceResult}\n\n" +
                $"Random Scoped Number:{scopedServiceResult}\n" +
                $"Random Scoped2 Number:{scopedServiceResult2}\n\n" +
                $"Random Transient Number:{transientServiceResult}\n" +
                $"Random Transient2 Number:{transientServiceResult2}";
            return Ok(result);
        }
    }
}