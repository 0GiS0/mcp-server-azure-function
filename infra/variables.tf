variable "location" {
  description = "The location where the resources will be created."
  type        = string
  default     = "northeurope"
}

variable "resource_group_name" {
  description = "The name of the resource group."
  type        = string
  default     = "azure-function-mcp-servers"
}

variable "function_app_name" {
  description = "The name of the function app."
  type        = string
  default     = "mcp-function-app"
}

variable "youtube_api_key" {
  description = "The API key for YouTube."
  type        = string
  sensitive   = true
}


variable "subscription_id" {
  description = "The Azure subscription ID."
  type        = string
  sensitive   = true
}
