param app_name string = 'ps-calculator'
param location string = resourceGroup().location

var app_plan_name = '${app_name}-plan'

resource app_plan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: app_plan_name
  location: location
  sku: {
    name: 'P0v3'
  }
}

resource app_name_resource 'Microsoft.Web/sites@2022-03-01' = {
  name: app_name
  location: location
  properties: {
    serverFarmId: app_plan.id
  }
}
