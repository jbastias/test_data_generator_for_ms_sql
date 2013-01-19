test_data_generator_for_ms_sql
==============================

using c# we create test data from an existing database using a linked server and the openrowset tsql command

test_data_generator_for_ms_sql/App.Config
--------------------------
See the file below as a starting point for app configuraton

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<startup> 
		<supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
	</startup>
  <connectionStrings>
	<add name="datatool" connectionString="Server={server};Database={database};Trusted_Connection=True;"/>
  </connectionStrings>
  <appSettings>
	<add key="server" value="[remote server]"/>
	<add key="database" value="[remote database]"/>
	<add key="username" value="[remote username]"/>
	<add key="password" value="[remote password]"/>
	<add key="schema" value="dbo"/>
  </appSettings>
</configuration>
```
