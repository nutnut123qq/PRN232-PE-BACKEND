#!/bin/bash
# Build script for Render deployment
# This script will restore, build, and run migrations

set -e  # Exit on error

echo "🔧 Restoring NuGet packages..."
dotnet restore

echo "🏗️ Building solution..."
dotnet build --configuration Release

echo "📦 Publishing WebAPI..."
dotnet publish src/WebAPI/WebAPI.csproj --configuration Release --output ./publish

echo "✅ Build completed successfully!"

# Note: Migrations should be run separately or in the start command
# Uncomment the following lines if you want to run migrations during build:
# echo "🗄️ Running database migrations..."
# cd src/WebAPI
# dotnet ef database update --no-build
# cd ../..

