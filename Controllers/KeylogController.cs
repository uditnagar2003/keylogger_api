﻿using keylogger_lib.DTO;
using keylogger_lib.Entities;
using Microsoft.AspNetCore.Mvc;
using serverLibrary.Respositories.contract;

namespace keylogger_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyloggerController(IGenericRepositoryInterface<KeyLoggerInfo> genericRepositoryInterface) : GenericController<KeyLoggerInfo>(genericRepositoryInterface)
    {
    }
}
