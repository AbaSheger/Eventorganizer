import 'package:latlong2/latlong.dart';

class EnvironmentalMarker {
  final String id;
  final String title;
  final String description;
  final LatLng location;
  final MarkerType type;
  final String iconUrl;

  EnvironmentalMarker({
    required this.id,
    required this.title,
    required this.description,
    required this.location,
    required this.type,
    required this.iconUrl,
  });

  factory EnvironmentalMarker.fromJson(Map<String, dynamic> json) {
    return EnvironmentalMarker(
      id: json['id'],
      title: json['title'],
      description: json['description'],
      location: LatLng(json['lat'], json['lng']),
      type: MarkerType.values.firstWhere(
        (e) => e.toString() == 'MarkerType.${json['type']}',
        orElse: () => MarkerType.other,
      ),
      iconUrl: json['iconUrl'] ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'title': title,
      'description': description,
      'lat': location.latitude,
      'lng': location.longitude,
      'type': type.toString().split('.').last,
      'iconUrl': iconUrl,
    };
  }
}

enum MarkerType {
  protectedArea,
  recyclingStation,
  pollutionSource,
  cleanupNeeded,
  forestZone,
  other,
}

extension MarkerTypeExtension on MarkerType {
  String get displayName {
    switch (this) {
      case MarkerType.protectedArea:
        return 'Skyddat område';
      case MarkerType.recyclingStation:
        return 'Återvinningsstation';
      case MarkerType.pollutionSource:
        return 'Föroreningskälla';
      case MarkerType.cleanupNeeded:
        return 'Behöver sanering';
      case MarkerType.forestZone:
        return 'Skogsområde';
      case MarkerType.other:
        return 'Övrigt';
    }
  }

  String get emoji {
    switch (this) {
      case MarkerType.protectedArea:
        return '🛡️';
      case MarkerType.recyclingStation:
        return '♻️';
      case MarkerType.pollutionSource:
        return '☢️';
      case MarkerType.cleanupNeeded:
        return '🧹';
      case MarkerType.forestZone:
        return '🌲';
      case MarkerType.other:
        return '📍';
    }
  }
}