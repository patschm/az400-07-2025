﻿Configuration WebConfig
{
    param
    (
        # Target nodes to apply the configuration
        [string[]]$NodeName = 'localhost'
    )
    # Import the module that defines custom resources
    Import-DscResource -Module xWebAdministration
    Node $NodeName
    {
        # Install the IIS role
        WindowsFeature IIS
        {
            Ensure          = "Present"
            Name            = "Web-Server"
        }
        #Install ASP.NET 4.5
        WindowsFeature ASPNet45
        {
          Ensure = “Present”
          Name = “Web-Asp-Net45”
        }
        # Stop the default website
        xWebsite DefaultSite
        {
            Ensure          = "Present"
            Name            = "Default Web Site"
            State           = "Stopped"
            PhysicalPath    = "C:\inetpub\wwwroot"
            DependsOn       = "[WindowsFeature]IIS"
        }
        # Copy the website content
        File WebContent
        {
            Ensure          = "Present"
            SourcePath      = "C:\Program Files\WindowsPowerShell\Modules\xWebAdministration\MySite"
            DestinationPath = "C:\inetpub\MySite"
            Recurse         = $true
            Type            = "Directory"
            DependsOn       = "[WindowsFeature]AspNet45"
        }
        # Create a new website
        xWebsite MyWebSite
        {
            Ensure          = "Present"
            Name            = "MySite"
            State           = "Started"
            PhysicalPath    = "C:\inetpub\MySite"
            DependsOn       = "[File]WebContent"
        }
    }
}