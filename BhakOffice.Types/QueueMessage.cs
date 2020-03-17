using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhakOffice.Types {

  [Serializable]
  public class QueueMessage {
    public String id { get; set; }
    public String topic { get; set; }
    public Object payload { get; set; }
  }
}
