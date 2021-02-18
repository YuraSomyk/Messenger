using System;
using System.Collections.Generic;
using System.Text;

namespace Messenger.BLL.Services.Password.Interface {

    public interface IPasswordService {

        string Hash(string clearText);
    }
}
