provider "azurerm" {
  features {}
}

provider "azuread" {
  tenant_id = "beb78ea8-cc42-4593-a1db-61b48468e933"
}

terraform {
    backend "azurerm"{
        resource_group_name = "Operational"
        storage_account_name = "dbszkolenieoperational"
        container_name = "terraformstate"
        key = "prod.terraform.tfstate"
        access_key = "kjrzEmp/fSImdAnts93xHBiAuetTW2fPWpmbhMlrvHSYEV29MVpfe5jvWASxeH0p96aXzZ6UGaPn+AStX4KMbg=="
    }
}

resource "azurerm_resource_group" "example" {
  name     = "myResourceGroup"
  location = "East US"
}

resource "azurerm_app_service_plan" "example" {
  name                = "myAppServicePlan"
  location            = azurerm_resource_group.example.location
  resource_group_name = azurerm_resource_group.example.name
  sku {
    tier = "Free"
    size = "F1"
  }
}

resource "azurerm_application_insights" "example" {
  name                = "myAppInsights"
  location            = azurerm_resource_group.example.location
  resource_group_name = azurerm_resource_group.example.name
  application_type    = "web"
}

resource "azurerm_app_service" "example" {
  name                = "myWebAppDBSzkolenie"
  location            = azurerm_resource_group.example.location
  resource_group_name = azurerm_resource_group.example.name
  app_service_plan_id = azurerm_app_service_plan.example.id

  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY" = azurerm_application_insights.example.instrumentation_key
    "ConnectionStrings:BlobConnectionString" = "DefaultEndpointsProtocol=https;AccountName=mystoragedbszkolenie;AccountKey=vosn4Qujq1GVxe6xwEQTtrSG0bRAvZhoBo97h82WBq5AsEKWVRIBzE4z6/1s6iSokbzLsG3E3HN8+AStDcPrjA==;EndpointSuffix=core.windows.net"
  }

  connection_string {
    name  = "DBConnectionString"
    type  = "SQLAzure"
    value = "Server=tcp:${azurerm_sql_server.example.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_sql_database.example.name};Persist Security Info=False;User ID=${azurerm_sql_server.example.administrator_login};Password=${azurerm_sql_server.example.administrator_login_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}

resource "azurerm_sql_server" "example" {
  name                         = "mysqlserverdbszkolenie"
  location                     = azurerm_resource_group.example.location
  resource_group_name          = azurerm_resource_group.example.name
  version                      = "12.0"
  administrator_login          = "myAdmin"
  administrator_login_password = "myPassword1234!"
}

resource "azurerm_sql_database" "example" {
  name                = "myDatabase"
  resource_group_name = azurerm_resource_group.example.name
  server_name         = azurerm_sql_server.example.name
  location            = azurerm_resource_group.example.location
  edition             = "Basic"
}

resource "azurerm_storage_account" "example" {
  name                      = "mystoragedbszkolenie"
  resource_group_name       = azurerm_resource_group.example.name
  location                  = azurerm_resource_group.example.location
  account_tier              = "Standard"
  account_replication_type  = "LRS"
  account_kind              = "StorageV2"
}

resource "azurerm_storage_container" "example" {
name                  = "mycontainer"
storage_account_name  = azurerm_storage_account.example.name
container_access_type = "private"
}

resource "azuread_application" "example" {
  display_name = azurerm_app_service.example.name

  web {
    homepage_url = "https://mywebappdbszkolenie.azurewebsites.net"
  }
}

resource "azuread_service_principal" "example" {
  application_id = azuread_application.example.application_id
}