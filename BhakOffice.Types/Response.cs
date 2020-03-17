using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhakOffice.Types {
  public class Response {
    protected Returns _type;
    protected Object _result;

    public Response(Returns type, Object result) {
      this._type = type;
      this._result = result;
    }

    public Boolean IsSuccess() {
      if (this._type == Returns.OK) {
        return true;
      } else {
        return false;
      }
    }

    public Object GetResult() {
      return this._result;
    }
  }
}
