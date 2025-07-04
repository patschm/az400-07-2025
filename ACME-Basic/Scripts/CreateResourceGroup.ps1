param([string]$ResourceGroupName = "test-rg", [string]$Location = "westeurope")

Get-AzResourceGroup -Name $ResourceGroupName -ErrorVariable notFound -ErrorAction SilentlyContinue
if ($notFound) {
    Write-Host "Creating resource group: $ResourceGroupName"
    New-AzResourceGroup -Name $ResourceGroupName -Location $Location
} else {
    Write-Host "Resource group '$ResourceGroupName' already exists."
}
