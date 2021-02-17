﻿using System;

namespace Messenger.BLL.Exceptions {

    public class ValidationException : Exception {

        public string Property { get; protected set; }

        public ValidationException(string message, string prop) : base(message) {
            Property = prop;
        }
    }
}