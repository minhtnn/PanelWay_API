using AutoMapper;
using PanelWay_Backend.API.Payload.Requests.Transactions;
using PanelWay_Backend.API.Payload.Responses.Transactions;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Mappers;

public class TransactionMapper : Profile
{
    public TransactionMapper()
    {
        CreateMap<CreateTransactionRequest, Transaction>();
        CreateMap<UpdateTransactionRequest, Transaction>();
        CreateMap<Transaction, TransactionResponse>();
    }
}