﻿namespace MotorcycleService.Infrastructure.Messaging;

public class RabbitMQConfiguration
{
    public string Host { get; set; }
    public string QueueYear2024 { get; set; }
    public string QueueRegistered { get; set; }
}