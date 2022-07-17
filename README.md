# Hypertheory Reference Application

This is the central repository for the reference application, and contains submodules for each of the apps.

## Setting up the Dev Environment

The [Docker Compose](.\docker-compose.yml) file contains the [[Infrastructure Services|InfrastructureServices]] needed to support the application.

> Note: Consider using https://github.com/conduktor/kafka-stack-docker-compose/blob/master/zk-single-kafka-single.yml

## Auth Stuff

Once Keycloak is up, log in with the admin account.

> Note: Might just have to regenerate the client secret.

On the **master** realm:

Find the `admin-cli` client.

Turn on `Service Accounts Enabled`

On `Service Account Roles` tab,

Select `Client Roles`.

Select from the list `hypertheory-realm`

add:

- `manage-users`
- `query-users`
- `view-users`
