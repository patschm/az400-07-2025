param app_name string = 'ps-calculator'
param location string = resourceGroup().location

@description('Comma seperated list of environments')
param environments array = [
  'development'
  'testing'
  'staging'
]

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

resource app_name_environments 'Microsoft.Web/sites/slots@2022-03-01' = [for item in environments: {
  parent: app_name_resource
  name: '${item}'
  location: location
  properties: {
    serverFarmId: app_plan.id
  }
}]
