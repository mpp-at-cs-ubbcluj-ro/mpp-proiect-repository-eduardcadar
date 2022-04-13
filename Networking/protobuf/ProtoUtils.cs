using m=Model;
using proto=Protobuf;
using Protobuf;
using System;
using System.Collections.Generic;

namespace Networking.protobuf
{
    public class ProtoUtils
    {
        public static Request CreateLoginRequest(m.Agency agency)
        {
            proto.Agency agencyProto = new() { Name = agency.Name, Password = agency.Password };
            Request request = new() { Type = Request.Types.Type.Login, Agency = agencyProto };
            return request;
        }

        public static Request CreateLogoutRequest(m.Agency agency)
        {
            proto.Agency agencyProto = new() { Name = agency.Name, Password = agency.Password };
            Request request = new() { Type = Request.Types.Type.Logout, Agency = agencyProto };
            return request;
        }

        public static Response CreateOkResponse()
        {
            return new Response { Type = Response.Types.Type.Ok };
        }

        public static Response CreateErrorResponse(string msg)
        {
            return new Response { Type = Response.Types.Type.Error, Error = msg };
        }

        public static m.Agency GetAgency(Request request)
        {
            return new m.Agency(request.Agency.Name, request.Agency.Password);
        }

        public static m.Reservation GetReservation(Request request)
        {
            proto.Trip tripProto = request.Reservation.Trip;
            m.Trip trip = new(tripProto.Id, tripProto.TouristAttraction,
                tripProto.TransportCompany, TimeSpan.Parse(tripProto.DepartureTime),
                tripProto.Price, tripProto.Seats);

            return new m.Reservation(request.Reservation.Client,
                trip,
                request.Reservation.Agency,
                request.Reservation.PhoneNumber,
                request.Reservation.Seats
            );
        }

        public static Response CreateNewReservationResponse(m.Reservation reservation)
        {
            proto.Trip tripProto = new()
            {
                Id = reservation.Trip.Id,
                TouristAttraction = reservation.Trip.TouristAttraction,
                TransportCompany = reservation.Trip.TransportCompany,
                DepartureTime = reservation.Trip.DepartureTime.ToString(),
                Price = reservation.Trip.Price,
                Seats = reservation.Trip.Seats
            };
            proto.Reservation reservationProto = new()
            {
                Client = reservation.Client,
                Trip = tripProto,
                Agency = reservation.Agency,
                PhoneNumber = reservation.PhoneNumber,
                Seats = reservation.Seats
            };
            return new()
            {
                Type = Response.Types.Type.NewReservation,
                Reservation = reservationProto
            };
        }

        public static dto.TripFilterDTO GetTripFilterDTO(Request request)
        {
            return new dto.TripFilterDTO
            {
                Destination = request.TripFilterDTO.Destination,
                StartTime = TimeSpan.Parse(request.TripFilterDTO.StartTime),
                EndTime = TimeSpan.Parse(request.TripFilterDTO.EndTime)
            };
        }

        public static Request CreateGetAgenciesRequest()
        {
            return new Request { Type = Request.Types.Type.GetAgencies };
        }

        public static m.Agency[] GetAgencies(Response response)
        {
            List<m.Agency> agencies = new();
            foreach (var agencyProto in response.Agencies)
            {
                agencies.Add(new m.Agency(agencyProto.Name, agencyProto.Password));
            }
            return agencies.ToArray();
        }

        public static Response CreateGetReservationsResponse(m.Reservation[] reservations)
        {
            Response response = new() { Type = Response.Types.Type.GetReservations };
            foreach (var reservation in reservations)
            {
                var trip = reservation.Trip;
                proto.Trip tripProto = new()
                {
                    Id = trip.Id,
                    TouristAttraction = trip.TouristAttraction,
                    TransportCompany = trip.TransportCompany,
                    DepartureTime = trip.DepartureTime.ToString(),
                    Seats = trip.Seats,
                    Price = trip.Price
                };
                proto.Reservation reservationProto = new()
                {
                    Client = reservation.Client,
                    Trip = tripProto,
                    Agency = reservation.Agency,
                    PhoneNumber = reservation.PhoneNumber,
                    Seats = reservation.Seats
                };
                response.Reservations.Add(reservationProto);
            }
            return response;
        }

        public static Request CreateSaveReservationRequest(m.Reservation reservation)
        {
            m.Trip trip = reservation.Trip;
            proto.Trip tripProto = new()
            {
                Id = trip.Id,
                TouristAttraction = trip.TouristAttraction,
                TransportCompany = trip.TransportCompany,
                DepartureTime = trip.DepartureTime.ToString(),
                Seats = trip.Seats,
                Price = trip.Price
            };
            proto.Reservation reservationProto = new()
            {
                Client = reservation.Client,
                Trip = tripProto,
                Agency = reservation.Agency,
                PhoneNumber = reservation.PhoneNumber,
                Seats = reservation.Seats
            };
            return new Request
            {
                Type = Request.Types.Type.SaveReservation,
                Reservation = reservationProto
            };
        }

        public static m.Reservation[] GetReservations(Response response)
        {
            List<m.Reservation> reservations = new();
            foreach (var reservationProto in response.Reservations)
            {
                proto.Trip tripProto = reservationProto.Trip;
                m.Trip trip = new(
                    tripProto.Id,
                    tripProto.TouristAttraction,
                    tripProto.TransportCompany,
                    TimeSpan.Parse(tripProto.DepartureTime),
                    tripProto.Price,
                    tripProto.Seats);
                reservations.Add(new m.Reservation(
                    reservationProto.Client,
                    trip,
                    reservationProto.Agency,
                    reservationProto.PhoneNumber,
                    reservationProto.Seats
                    ));
            }
            return reservations.ToArray();
        }

        public static Request CreateGetReservationsRequest()
        {
            return new Request { Type = Request.Types.Type.GetReservations };
        }

        public static m.Trip[] GetTrips(Response response)
        {
            List<m.Trip> trips = new();
            foreach (var tripProto in response.Trips)
            {
                trips.Add(new m.Trip(
                    tripProto.Id,
                    tripProto.TouristAttraction,
                    tripProto.TransportCompany,
                    TimeSpan.Parse(tripProto.DepartureTime),
                    tripProto.Price,
                    tripProto.Seats
                    ));
            }
            return trips.ToArray();
        }

        public static Request CreateGetTripsRequest(dto.TripFilterDTO filterDTO)
        {
            proto.TripFilterDTO tripFilterProto = new()
            {
                Destination = filterDTO.Destination,
                StartTime = filterDTO.StartTime.ToString(),
                EndTime = filterDTO.EndTime.ToString()
            };
            return new Request
            {
                Type = Request.Types.Type.GetTrips,
                TripFilterDTO = tripFilterProto
            };
        }

        public static Response CreateGetTripsResponse(m.Trip[] trips)
        {
            Response response = new() { Type = Response.Types.Type.GetTrips };
            foreach (var trip in trips)
            {
                proto.Trip tripProto = new()
                {
                    Id = trip.Id,
                    TouristAttraction = trip.TouristAttraction,
                    TransportCompany = trip.TransportCompany,
                    DepartureTime = trip.DepartureTime.ToString(),
                    Seats = trip.Seats,
                    Price = trip.Price
                };
                response.Trips.Add(tripProto);
            }
            return response;
        }

        public static Response CreateGetAgenciesResponse(m.Agency[] agencies)
        {
            Response response = new() { Type = Response.Types.Type.GetAgencies };
            foreach (var agency in agencies)
            {
                proto.Agency agencyProto = new()
                {
                    Name = agency.Name,
                    Password = agency.Password
                };
                response.Agencies.Add(agencyProto);
            }
            return response;
        }

        public static m.Reservation GetReservation(Response response)
        {
            proto.Trip tripProto = response.Reservation.Trip;
            m.Trip trip = new(tripProto.Id, tripProto.TouristAttraction,
                tripProto.TransportCompany, TimeSpan.Parse(tripProto.DepartureTime),
                tripProto.Price, tripProto.Seats);

            return new m.Reservation(response.Reservation.Client,
                trip,
                response.Reservation.Agency,
                response.Reservation.PhoneNumber,
                response.Reservation.Seats
            );
        }
    }
}
