﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
  public interface IUnitOfWork : IDisposable
  {
    void Save();
    void Commit();
    void Rollback();
  }
}
