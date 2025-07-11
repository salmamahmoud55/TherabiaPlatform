# المرحلة الأولى: قاعدة التشغيل (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# المرحلة الثانية: البناء (build)
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# نسخ كل الملفات للمجلد الحالي
COPY . .

# استعادة الحزم (dependencies)
RUN dotnet restore "therabia.csproj"

# بناء المشروع
RUN dotnet build "therabia.csproj" -c Release -o /app/build

# نشر المشروع
RUN dotnet publish "therabia.csproj" -c Release -o /app/publish

# المرحلة الأخيرة: التشغيل النهائي
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "therabia.dll"]