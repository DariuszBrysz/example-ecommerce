provider "azurerm" {
  features {}
}

ferraform {
    backend "azurerm"{
        resource_group_name = "Operational"
        storage_account_name = "dbszkolenieoperational"
        container_name = "terraformstate"
        key = "prod.terraform.tfstate"
        access_key = "kjrzEmp/fSImdAnts93xHBiAuetTW2fPWpmbhMlrvHSYEV29MVpfe5jvWASxeH0p96aXzZ6UGaPn+AStX4KMbg=="
    }
}

resource "azurerm_resource_group" "rg" {
  name     = "MyResourceGroup"
  location = "East US"
}