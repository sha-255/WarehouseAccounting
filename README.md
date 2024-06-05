# WarehouseAccounting

## Deploying:

In `appSettings.cfg`:

```
Data Source=
[You data source]
;Initial Catalog=
[You Initial catalog]
;Integrated Security=True;Encrypt=False
```

In your SMS, use your own .bacpac/tsql/create the database yourself, and specify this database in the configuration file as [You Initial catalog] and You Sql Server name as [You data source].
