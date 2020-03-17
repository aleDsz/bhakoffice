using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhakOffice.Types {
  public class Response {
    public Returns type { get; set; }
    public Object result { get; set; }

    public Response(Returns type, Object result) {
      this.type = type;
      this.result = result;
    }

    public Boolean IsSuccess() {
      if (this.type == Returns.OK) {
        return true;
      } else {
        return false;
      }
    }

    public Object GetResult() {
      return this.result;
    }
  }
}
