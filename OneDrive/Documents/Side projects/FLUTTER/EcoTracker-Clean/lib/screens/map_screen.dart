import 'package:flutter/material.dart';
import 'package:flutter_map/flutter_map.dart';
import 'package:geolocator/geolocator.dart';
import 'package:latlong2/latlong.dart';
import 'package:permission_handler/permission_handler.dart';
import '../models/environmental_marker.dart';
import '../widgets/location_marker.dart';

class MapScreen extends StatefulWidget {
  const MapScreen({super.key});

  @override
  State<MapScreen> createState() => _MapScreenState();
}

class _MapScreenState extends State<MapScreen> {
  final MapController _mapController = MapController();
  Position? _currentPosition;
  bool _isLoading = true;
  bool _locationPermissionGranted = false;
  
  // Sample environmental markers in Sweden
  final List<EnvironmentalMarker> _environmentalMarkers = [
    EnvironmentalMarker(
      id: '1',
      title: 'Tyresta Nationalpark',
      description: 'Skyddat naturområde med ursprunglig granskog och värdefull biologisk mångfald. Ett av Sveriges mest välbevarade naturskogsområden.',
      location: const LatLng(59.1667, 18.2333),
      type: MarkerType.protectedArea,
      iconUrl: '',
    ),
    EnvironmentalMarker(
      id: '2',
      title: 'Återvinningscentral Jordbro',
      description: 'Modern återvinningsanläggning med sortering för alla typer av hushållsavfall. Öppet vardagar 07:00-19:00.',
      location: const LatLng(59.2167, 17.9833),
      type: MarkerType.recyclingStation,
      iconUrl: '',
    ),
    EnvironmentalMarker(
      id: '3',
      title: 'Gamla industriområdet',
      description: 'Tidigare industriell verksamhet har lett till markföroreningar. Pågående miljöundersökning och riskbedömning.',
      location: const LatLng(59.3500, 18.0500),
      type: MarkerType.pollutionSource,
      iconUrl: '',
    ),
    EnvironmentalMarker(
      id: '4',
      title: 'Södermalms grönområde',
      description: 'Parkområde som behöver skötsel och sophantering. Lokala initiativ för städning pågår.',
      location: const LatLng(59.3167, 18.0667),
      type: MarkerType.cleanupNeeded,
      iconUrl: '',
    ),
    EnvironmentalMarker(
      id: '5',
      title: 'Nacka Naturreservat',
      description: 'Omfattande skogsreservat med vandringsleder och rikt djurliv. Viktigt för stadens luftkvalitet och klimat.',
      location: const LatLng(59.3000, 18.1667),
      type: MarkerType.forestZone,
      iconUrl: '',
    ),
  ];

  @override
  void initState() {
    super.initState();
    _requestLocationPermission();
  }

  Future<void> _requestLocationPermission() async {
    setState(() => _isLoading = true);
    
    try {
      final status = await Permission.location.request();
      
      if (status.isGranted) {
        setState(() => _locationPermissionGranted = true);
        await _getCurrentLocation();
      } else {
        setState(() {
          _locationPermissionGranted = false;
          _isLoading = false;
        });
        
        if (mounted) {
          _showLocationPermissionDialog();
        }
      }
    } catch (e) {
      setState(() {
        _locationPermissionGranted = false;
        _isLoading = false;
      });
      
      if (mounted) {
        _showLocationErrorDialog();
      }
    }
  }

  Future<void> _getCurrentLocation() async {
    try {
      final position = await Geolocator.getCurrentPosition(
        desiredAccuracy: LocationAccuracy.high,
        timeLimit: const Duration(seconds: 10),
      );
      
      setState(() {
        _currentPosition = position;
        _isLoading = false;
      });
      
      // Center map on current location
      _mapController.move(
        LatLng(position.latitude, position.longitude),
        13.0,
      );
    } catch (e) {
      setState(() => _isLoading = false);
      
      if (mounted) {
        _showLocationErrorDialog();
      }
    }
  }

  void _centerOnCurrentLocation() async {
    if (_currentPosition != null) {
      _mapController.move(
        LatLng(_currentPosition!.latitude, _currentPosition!.longitude),
        15.0,
      );
    } else {
      await _getCurrentLocation();
    }
  }

  void _showLocationPermissionDialog() {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Platsbehörighet krävs'),
        content: const Text(
          'EcoTracker behöver åtkomst till din plats för att visa miljödata och din position på kartan. '
          'Gå till inställningar och aktivera platsbehörighet för appen.',
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.of(context).pop(),
            child: const Text('Avbryt'),
          ),
          ElevatedButton(
            onPressed: () {
              Navigator.of(context).pop();
              openAppSettings();
            },
            child: const Text('Inställningar'),
          ),
        ],
      ),
    );
  }

  void _showLocationErrorDialog() {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Platsfel'),
        content: const Text(
          'Kunde inte hämta din aktuella position. Kontrollera att GPS är aktiverat och försök igen.',
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.of(context).pop(),
            child: const Text('OK'),
          ),
          ElevatedButton(
            onPressed: () {
              Navigator.of(context).pop();
              _getCurrentLocation();
            },
            child: const Text('Försök igen'),
          ),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text(
          'EcoTracker',
          style: TextStyle(
            fontWeight: FontWeight.bold,
            color: Colors.white,
          ),
        ),
        backgroundColor: Colors.green.shade700,
        elevation: 2,
        actions: [
          IconButton(
            icon: const Icon(Icons.info_outline, color: Colors.white),
            onPressed: _showAppInfo,
          ),
        ],
      ),
      body: _isLoading
          ? const Center(
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  CircularProgressIndicator(
                    valueColor: AlwaysStoppedAnimation<Color>(Colors.green),
                  ),
                  SizedBox(height: 16),
                  Text(
                    'Hämtar platsdata...',
                    style: TextStyle(fontSize: 16),
                  ),
                ],
              ),
            )
          : FlutterMap(
              mapController: _mapController,
              options: MapOptions(
                initialCenter: _currentPosition != null
                    ? LatLng(_currentPosition!.latitude, _currentPosition!.longitude)
                    : const LatLng(59.3293, 18.0686), // Stockholm as fallback
                initialZoom: 11.0,
                minZoom: 5.0,
                maxZoom: 18.0,
                interactionOptions: const InteractionOptions(
                  flags: InteractiveFlag.all,
                ),
              ),
              children: [
                TileLayer(
                  urlTemplate: 'https://tile.openstreetmap.org/{z}/{x}/{y}.png',
                  userAgentPackageName: 'com.example.ecotracker',
                  maxZoom: 19,
                ),
                // Environmental markers layer
                MarkerLayer(
                  markers: _environmentalMarkers.map((envMarker) {
                    return Marker(
                      point: envMarker.location,
                      width: 40,
                      height: 40,
                      child: LocationMarkerWidget(marker: envMarker),
                    );
                  }).toList(),
                ),
                // Current location marker layer
                if (_currentPosition != null && _locationPermissionGranted)
                  MarkerLayer(
                    markers: [
                      Marker(
                        point: LatLng(_currentPosition!.latitude, _currentPosition!.longitude),
                        width: 20,
                        height: 20,
                        child: Container(
                          decoration: BoxDecoration(
                            color: Colors.blue.shade600,
                            shape: BoxShape.circle,
                            border: Border.all(color: Colors.white, width: 3),
                            boxShadow: [
                              BoxShadow(
                                color: Colors.black.withOpacity(0.3),
                                blurRadius: 4,
                                offset: const Offset(0, 2),
                              ),
                            ],
                          ),
                        ),
                      ),
                    ],
                  ),
              ],
            ),
      floatingActionButton: Column(
        mainAxisAlignment: MainAxisAlignment.end,
        children: [
          FloatingActionButton(
            heroTag: "legend",
            onPressed: _showLegend,
            backgroundColor: Colors.white,
            foregroundColor: Colors.green.shade700,
            mini: true,
            child: const Icon(Icons.legend_toggle),
          ),
          const SizedBox(height: 12),
          FloatingActionButton(
            heroTag: "location",
            onPressed: _centerOnCurrentLocation,
            backgroundColor: Colors.green.shade700,
            foregroundColor: Colors.white,
            child: const Icon(Icons.my_location),
          ),
        ],
      ),
    );
  }

  void _showLegend() {
    showModalBottomSheet(
      context: context,
      backgroundColor: Colors.transparent,
      builder: (context) => Container(
        margin: const EdgeInsets.all(16),
        decoration: BoxDecoration(
          color: Theme.of(context).scaffoldBackgroundColor,
          borderRadius: BorderRadius.circular(16),
        ),
        child: Padding(
          padding: const EdgeInsets.all(20),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Miljömarkörer',
                style: Theme.of(context).textTheme.headlineSmall?.copyWith(
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(height: 16),
              ...MarkerType.values.map((type) => Padding(
                padding: const EdgeInsets.only(bottom: 12),
                child: Row(
                  children: [
                    Text(
                      type.emoji,
                      style: const TextStyle(fontSize: 20),
                    ),
                    const SizedBox(width: 12),
                    Text(
                      type.displayName,
                      style: Theme.of(context).textTheme.bodyLarge,
                    ),
                  ],
                ),
              )),
              const SizedBox(height: 8),
              Row(
                children: [
                  Container(
                    width: 16,
                    height: 16,
                    decoration: BoxDecoration(
                      color: Colors.blue.shade600,
                      shape: BoxShape.circle,
                      border: Border.all(color: Colors.white, width: 2),
                    ),
                  ),
                  const SizedBox(width: 12),
                  Text(
                    'Din aktuella position',
                    style: Theme.of(context).textTheme.bodyLarge,
                  ),
                ],
              ),
            ],
          ),
        ),
      ),
    );
  }

  void _showAppInfo() {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Om EcoTracker'),
        content: const Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              'EcoTracker är en demonstrationsapp som visar miljödata och GPS-funktionalitet med Flutter.',
            ),
            SizedBox(height: 12),
            Text(
              'Utvecklad för att utforska Flutter, geolocalisering och Flutter Map-integration.',
            ),
            SizedBox(height: 12),
            Text(
              'Appen visar olika miljörelaterade platser som skyddade områden, återvinningsstationer och områden som behöver uppmärksamhet.',
            ),
          ],
        ),
        actions: [
          ElevatedButton(
            onPressed: () => Navigator.of(context).pop(),
            child: const Text('OK'),
          ),
        ],
      ),
    );
  }
}