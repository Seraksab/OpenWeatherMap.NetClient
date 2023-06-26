# Changelog

All notable changes to this project will be documented in this file.  
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)

## 1.3.1 - 2023-06-26

### Changed

* Use DateTimeOffset instead of DateTime for timestamps
* Updated UnitsNet dependency

## 1.3.0 - 2023-06-16

### Added

* Expose the HttpClient instance to allow custom configuration

### Changed

* No longer wrap Refit exceptions
* Updated dependencies

## 1.2.0 - 2022-09-23

### Added

* Support for retries

### Changed

* Internal handling of API calls
* Utilize Polly and MemoryCache for caching instead of LazyCache

## 1.1.0 - 2022-08-26

### Added

* Support for the 'Basic weather maps' API
* Support for the '3-hour Forecast 5 days' API
* Missing 'ground level pressure' and 'sea level pressure' measurements to CurrentWeather
* Missing 'wind gust' measurement to CurrentWeather

### Changed

* Instead of wrapping API results and exceptions in an ApiResponse, return or throw them directly.
* Renamed some functions of the AirPollution and CurrentWeather API

## 1.0.3 - 2022-07-30

First public release