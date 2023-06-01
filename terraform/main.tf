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

resource "azurerm_app_service" "example" {
  name                = "myWebAppDBSzkolenie"
  location            = azurerm_resource_group.example.location
  resource_group_name = azurerm_resource_group.example.name
  app_service_plan_id = azurerm_app_service_plan.example.id
}