using Domain.Entity;
using Infrastructure.Context.command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Services.Base;

public class ReminderService : BackgroundService
	{
		private readonly ILogger<ReminderService> _logger;
		private readonly IServiceProvider _services;

		public ReminderService(ILogger<ReminderService> logger, IServiceProvider services)
		{
			_logger = logger;
			_services = services;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					using (var scope = _services.CreateScope())
					{
						var dbContext = scope.ServiceProvider.GetRequiredService<CommandDataContext>();
						var reminders = dbContext.Reminder
							.Where(r => !r.Sent && r.ScheduledTime <= DateTime.Now)
							.ToList();

						foreach (var reminder in reminders)
						{
							await SendReminderAsync(reminder);
							reminder.Sent = true;
							dbContext.SaveChanges();
						}
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "خطا در ارسال یادآوری‌ها");
				}

				await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // هر ۱ دقیقه بررسی کند
			}
		}

		private async Task SendReminderAsync(Reminder reminder)
		{
			if (reminder.ReminderType == "Email")
			{
				await SendEmailReminderAsync(reminder);
			}
			else if (reminder.ReminderType == "SMS")
			{
				await SendSmsReminderAsync(reminder);
			}
		}

		private async Task SendEmailReminderAsync(Reminder reminder)
		{
			// ارسال ایمیل با استفاده از SendGrid یا سرویس‌های دیگر
			// مثال:
			//var emailService = new EmailService();
			//var userEmail = GetUserEmail(reminder.PersonId);
			//var sessionDetails = GetMeetingDetails(reminder.SessionId);

			//var subject = "یادآوری جلسه";
			//var body = $"جلسه‌ی '{sessionDetails.Title}' در تاریخ {sessionDetails.StartDateTime} برگزار می‌شود.";

			//await emailService.SendEmailAsync(userEmail, subject, body);
		}

		private async Task SendSmsReminderAsync(Reminder reminder)
		{
			// ارسال پیامک با استفاده از Twilio یا سرویس‌های دیگر
			// مثال:
			//var smsService = new SmsService();
			//var userPhoneNumber = GetUserPhoneNumber(reminder.PersonId);
			//var sessionDetails = GetMeetingDetails(reminder.SessionId);

			//var message = $"یادآوری: جلسه‌ی '{sessionDetails.Title}' در تاریخ {sessionDetails.StartDateTime} برگزار می‌شود.";

			//await smsService.SendSmsAsync(userPhoneNumber, message);
		}
	}

