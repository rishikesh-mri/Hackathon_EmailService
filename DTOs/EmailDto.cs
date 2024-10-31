using System;

namespace EmailService.DTOs;

record class EmailDto(string email, string subject, string body);
