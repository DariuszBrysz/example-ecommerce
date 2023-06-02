provider "azurerm" {
  features {}
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
  }
}

resource "azurerm_sql_server" "example" {
  name                         = "mysqlserver"
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
