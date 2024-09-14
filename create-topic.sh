#!/bin/bash

# Esperar a que Kafka esté completamente inicializado
echo "Waiting for Kafka to be ready..."
cub kafka-ready -b kafka:9092 1 20

# Crear el tópico 'date'
echo "Creating topic 'date'..."
kafka-topics --create --topic date --partitions 1 --replication-factor 1 --if-not-exists --bootstrap-server kafka:9092
