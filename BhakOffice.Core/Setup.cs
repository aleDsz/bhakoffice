using System;
using System.Configuration;
using System.Collections.Generic;
using System.Messaging;
using BhakOffice.Types;

namespace BhakOffice.Core {
  public class Setup {
    public static Response Install(Returns _type = Returns.OK) {
      try {
        if (_type == Returns.Error) {
          return new Response(Returns.Error, "Can't install this bullshit");
        }

        var path = Environment.CurrentDirectory;
        var config_map = new ExeConfigurationFileMap();
        config_map.ExeConfigFilename = String.Format("{0}\\BhakOffice.Core.config", path);
        var configuration = ConfigurationManager.OpenMappedExeConfiguration(config_map, ConfigurationUserLevel.None);

        var queues = configuration.AppSettings.Settings["queues"].Value.ToString().Split(',');
        var message_queues = new List<MessageQueue>();

        foreach (var queue in queues) {
          var queue_name = String.Format(".\\private$\\{0}", queue);
          var message_queue = new MessageQueue();

          if (!MessageQueue.Exists(queue_name)) {
            message_queue = MessageQueue.Create(queue_name, true);
          } else {
            message_queue = new MessageQueue(queue_name);
          }

          message_queues.Add(message_queue);
        }

        return new Response(Returns.OK, message_queues);
      } catch (Exception ex) {
        return new Response(Returns.Error, ex.Message);
      }
    }
  }
} 
