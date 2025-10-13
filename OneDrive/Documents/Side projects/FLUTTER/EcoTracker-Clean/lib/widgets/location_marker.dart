import 'package:flutter/material.dart';
import '../models/environmental_marker.dart';

class LocationMarkerWidget extends StatelessWidget {
  final EnvironmentalMarker marker;
  final VoidCallback? onTap;

  const LocationMarkerWidget({
    super.key,
    required this.marker,
    this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        _showMarkerDetails(context);
        onTap?.call();
      },
      child: Container(
        width: 40,
        height: 40,
        decoration: BoxDecoration(
          color: _getMarkerColor(),
          shape: BoxShape.circle,
          border: Border.all(color: Colors.white, width: 2),
          boxShadow: [
            BoxShadow(
              color: Colors.black.withOpacity(0.3),
              blurRadius: 4,
              offset: const Offset(0, 2),
            ),
          ],
        ),
        child: Center(
          child: Text(
            marker.type.emoji,
            style: const TextStyle(fontSize: 18),
          ),
        ),
      ),
    );
  }

  Color _getMarkerColor() {
    switch (marker.type) {
      case MarkerType.protectedArea:
        return Colors.green.shade600;
      case MarkerType.recyclingStation:
        return Colors.blue.shade600;
      case MarkerType.pollutionSource:
        return Colors.red.shade600;
      case MarkerType.cleanupNeeded:
        return Colors.orange.shade600;
      case MarkerType.forestZone:
        return Colors.green.shade800;
      case MarkerType.other:
        return Colors.grey.shade600;
    }
  }

  void _showMarkerDetails(BuildContext context) {
    showModalBottomSheet(
      context: context,
      backgroundColor: Colors.transparent,
      builder: (context) => Container(
        margin: const EdgeInsets.all(16),
        decoration: BoxDecoration(
          color: Theme.of(context).scaffoldBackgroundColor,
          borderRadius: BorderRadius.circular(16),
          boxShadow: [
            BoxShadow(
              color: Colors.black.withOpacity(0.1),
              blurRadius: 10,
              offset: const Offset(0, -2),
            ),
          ],
        ),
        child: Padding(
          padding: const EdgeInsets.all(20),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Row(
                children: [
                  Container(
                    width: 40,
                    height: 40,
                    decoration: BoxDecoration(
                      color: _getMarkerColor(),
                      shape: BoxShape.circle,
                    ),
                    child: Center(
                      child: Text(
                        marker.type.emoji,
                        style: const TextStyle(fontSize: 20),
                      ),
                    ),
                  ),
                  const SizedBox(width: 12),
                  Expanded(
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          marker.title,
                          style: Theme.of(context).textTheme.headlineSmall?.copyWith(
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        Text(
                          marker.type.displayName,
                          style: Theme.of(context).textTheme.bodyMedium?.copyWith(
                            color: _getMarkerColor(),
                            fontWeight: FontWeight.w500,
                          ),
                        ),
                      ],
                    ),
                  ),
                ],
              ),
              const SizedBox(height: 16),
              Text(
                marker.description,
                style: Theme.of(context).textTheme.bodyLarge,
              ),
              const SizedBox(height: 16),
              Row(
                children: [
                  Icon(
                    Icons.location_on,
                    color: Colors.grey.shade600,
                    size: 16,
                  ),
                  const SizedBox(width: 4),
                  Text(
                    'Lat: ${marker.location.latitude.toStringAsFixed(4)}, '
                    'Lng: ${marker.location.longitude.toStringAsFixed(4)}',
                    style: Theme.of(context).textTheme.bodySmall?.copyWith(
                      color: Colors.grey.shade600,
                    ),
                  ),
                ],
              ),
              const SizedBox(height: 20),
              SizedBox(
                width: double.infinity,
                child: ElevatedButton(
                  onPressed: () => Navigator.of(context).pop(),
                  style: ElevatedButton.styleFrom(
                    backgroundColor: _getMarkerColor(),
                    foregroundColor: Colors.white,
                    padding: const EdgeInsets.symmetric(vertical: 12),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(8),
                    ),
                  ),
                  child: const Text('Stäng'),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}