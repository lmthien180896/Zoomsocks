using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Zoomsocks.WebUI.Shared.Mvc
{
    public static class ControllerExtensions
    {
        public const string PreparedMessageListKey = "__PreparedMessageList";

        public static string RemoveController(this string fullControllerClassName)
        {
            return fullControllerClassName.EndsWith("Controller", StringComparison.Ordinal)
                ? fullControllerClassName.Substring(0, fullControllerClassName.Length - 10)
                : fullControllerClassName;
        }

        public static string RenderPartialViewToString(this Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult =
                    ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext,
                    viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public static void PrepareInfoMessage(this ControllerBase controller, string message)
        {
            controller.PrepareMessage(PreparedMessageType.Info, message);
        }

        public static void PrepareSuccessMessage(this ControllerBase controller, string message)
        {
            controller.PrepareMessage(PreparedMessageType.Success, message);
        }

        public static void PrepareWarningMessage(this ControllerBase controller, string message)
        {
            controller.PrepareMessage(PreparedMessageType.Warning, message);
        }

        public static void PrepareWarningMessages(this ControllerBase controller, IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                controller.PrepareWarningMessage(message);
            }
        }       

        public static void PrepareErrorMessage(this ControllerBase controller, string message)
        {
            controller.PrepareMessage(PreparedMessageType.Error, message);
        }

        public static void PrepareErrorMessages(this ControllerBase controller, IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                controller.PrepareErrorMessage(message);
            }
        }        

        private static void PrepareMessage(this ControllerBase controller, PreparedMessageType type, string message)
        {
            var preparedMessageList = controller.TempData[PreparedMessageListKey] as List<PreparedMessage>;

            if (preparedMessageList == null)
            {
                preparedMessageList = new List<PreparedMessage>();
            }

            preparedMessageList.Add(
                new PreparedMessage { Type = type, Message = message });

            controller.TempData[PreparedMessageListKey] = preparedMessageList;
        }

        public enum PreparedMessageType : byte
        {
            Info,
            Success,
            Warning,
            Error
        }

        [Serializable]
        public class PreparedMessage
        {
            public PreparedMessageType Type { get; set; }

            public string Message { get; set; }
        }
    }
}
