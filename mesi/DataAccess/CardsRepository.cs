﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using mesi.Models;
using Microsoft.Extensions.Configuration;

namespace mesi.DataAccess
{
    public class CardsRepository
    {
        readonly string _connectionString;

        public CardsRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MESI");
        }

        internal IEnumerable<CardsWithDetail> GetCardsWithDetails(Guid householdId)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT C.Id AS CardId, C.AssignedUserId, C.HouseholdId, C.NeedTypeId, C.CategoryTypeId, C.CardName,
				                C.CardImage, C.CardDefinition, C.Conception, C.Planning, C. Execution, C.MSOC, C.DailyGrind,
				                 N.NeedTypeName, CT.CategoryTypeName
	                        FROM Cards C
		                        JOIN NeedTypes N ON N.Id = C.NeedTypeId
		                        JOIN CategoryTypes CT ON CT.Id = C.CategoryTypeId
                            WHERE HouseholdId = @HouseholdId";
            var parameter = new
            {
                HouseholdId = householdId,
            };
            var result = db.Query<CardsWithDetail>(sql, parameter);
            return result;
        }

        internal CardsWithDetail GetSingleCardWithDetails(Guid cardId)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT C.Id AS CardId, C.AssignedUserId, C.HouseholdId, C.NeedTypeId, C.CategoryTypeId, C.CardName,
				                C.CardImage, C.CardDefinition, C.Conception, C.Planning, C. Execution, C.MSOC, C.DailyGrind,
				                N.NeedTypeName, CT.CategoryTypeName
	                        FROM Cards C
		                        JOIN NeedTypes N ON N.Id = C.NeedTypeId
		                        JOIN CategoryTypes CT ON CT.Id = C.CategoryTypeId
                            WHERE C.Id = @CardId";
            var parameter = new
            {
                CardId = cardId,
            };
            var result = db.QuerySingleOrDefault<CardsWithDetail>(sql, parameter);
            return result;
        }

        internal Cards GetSingleCard(Guid id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM Cards
                            WHERE Id = @Id";
            var parameter = new
            {
                Id = id,
            };
            var result = db.QuerySingleOrDefault<Cards>(sql, parameter);
            return result;
        }

        internal IEnumerable<Cards> GetSingleCardByHHId(Guid householdId)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"SELECT * FROM Cards
                            WHERE HouseholdId = @householdId";
            var parameter = new
            {
                HouseholdId = householdId,
            };
            var result = db.Query<Cards>(sql, parameter);
            return result;
        }

        internal void AddIndividualCard(Cards cards)
        {
            using var db = new SqlConnection(_connectionString);
            Guid cardId = new Guid();

            var sql = @"INSERT INTO [dbo].[Cards]
                        ([HouseholdId],
                         [AssignedUserId],
                         [NeedTypeId],
                         [CategoryTypeId],
                         [CardName],
                         [CardImage],
                         [CardDefinition],
                         [Conception],
                         [Planning],
                         [Execution],
                         [MSOC],
                         [DailyGrind])
                        OUTPUT inserted.Id
                        VALUES
                       (@HouseholdId,
                        @AssignedUserId,
                        @NeedTypeId,
                        @CategoryTypeId,
                        @CardName,
                        @CardImage,
                        @CardDefinition,
                        @Conception,
                        @Planning,
                        @Execution,
                        @MSOC,
                        @DailyGrind)";

            cardId = db.ExecuteScalar<Guid>(sql, cards);
            cards.CardId = cardId;
        }

        internal Cards EditIndividualCard(Guid cardId, Cards cards)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"UPDATE Cards
                        SET HouseholdId = @HouseholdId, 
                            AssignedUserId = @AssignedUserId,
                            NeedTypeId = @NeedTypeId,
                            CategoryTypeId = @CategoryTypeId,
                            CardName = @CardName,
                            CardImage = @CardImage,
                            CardDefinition = @CardDefinition,
                            Conception = @Conception,
                            Planning = @Planning,
                            Execution = @Execution,
                            MSOC = @MSOC,
                            DailyGrind = @DailyGrind
                        WHERE Id = @CardId";

            cards.CardId = cardId;
            var updatedCard = db.QuerySingleOrDefault<Cards>(sql, cards);

            return updatedCard;
        }

        internal bool DeleteCard(Guid id)
        {
            bool returnVal = false;
            using var db = new SqlConnection(_connectionString);
            var sql = @"DELETE FROM Cards
                        OUTPUT Deleted.Id
                        WHERE Id = @Id";
            var parameters = new
            {
                Id = id
            };

            var result = db.Query(sql, parameters);
            if (result.Count() > 0)
            {
                returnVal = true;
            }
            return returnVal;
        }

        internal List<CardsWithDetail> getAllButDeclarations(Guid userId, Guid houseHoldId)
        {
            using var db = new SqlConnection(_connectionString);

            //First get a list of cards that the user has declared as their values
            var firstSql = @"SELECT C.Id as CardID, C.HouseholdId as HouseHoldId, C.NeedTypeId as NeedTypeId, C.CategoryTypeId as CategoryTypeId, C.AssignedUserId as AssignedUserId, C.CardName as CardName, C.CardImage as CardImage, C.CardDefinition as CardDefinition, C.Conception as Conception, C.Planning as Planning, C.Execution as Execution, C.MSOC as MSOC, C.DailyGrind as DailyGrind, N.NeedTypeName, CT.CategoryTypeName
                                FROM CARDS C
                                LEFT JOIN UserDeclaration D
                                ON C.Id = D.CardID
		                        JOIN NeedTypes N ON N.Id = C.NeedTypeId
		                        JOIN CategoryTypes CT ON CT.Id = C.CategoryTypeId
                                WHERE (D.UserID IS NULL AND C.HouseholdId =@houseHoldId) OR (D.UserID NOT LIKE @userID AND C.HouseholdId=@houseHoldId)";

            var paramObj = new
            {
                userId,
                houseHoldId
            };

            var listOfDeclarations = db.Query<CardsWithDetail>(firstSql, paramObj).ToList();

            return listOfDeclarations;
        }

	}
}
