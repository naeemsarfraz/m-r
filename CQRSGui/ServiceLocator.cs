using SimpleCQRS;

namespace CQRSGui
{
    public static class ServiceLocator
    {
        public static MediatR.Mediator Mediator { get; set; }
        public static FakeBus Bus { get; set; }
       
    }
}