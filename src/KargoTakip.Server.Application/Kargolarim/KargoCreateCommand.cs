using FluentValidation;
using GenericRepository;
using KargoTakip.Server.Domain.Kargolarim;
using Mapster;
using MediatR;
using TS.Result;

namespace KargoTakip.Server.Application.Kargolarim;

public sealed record KargoCreateCommand(
	Person Gonderen,
	Person Alici,
	Address TeslimAdresi,
	KargoInformationDto KargoInformation) :IRequest<Result<string>>;

public sealed record KargoInformationDto(
	int KargoTipiValue,
	int Agirlik);

//bazı bilgilerin kalici olmasini istediğim için;
public sealed class KargoCreateCommandValidator : AbstractValidator<KargoCreateCommand>
{
	public KargoCreateCommandValidator()
	{
		RuleFor(p => p.Gonderen.FirstName).NotEmpty().WithMessage("Geçerli bir gönderen adı giriniz!");
		RuleFor(p => p.Gonderen.LastName).NotEmpty().WithMessage("Geçerli bir gönderen soyadı giriniz!");
		RuleFor(p => p.Alici.FirstName).NotEmpty().WithMessage("Geçerli bir alıcı adı giriniz!");
		RuleFor(p => p.Alici.LastName).NotEmpty().WithMessage("Geçerli bir alıcı adı giriniz!");
		RuleFor(p => p.TeslimAdresi.City).NotEmpty().WithMessage("Geçerli bir şehir giriniz!");
		RuleFor(p => p.TeslimAdresi.Town).NotEmpty().WithMessage("Geçerli bir ilçe giriniz!");
		RuleFor(p => p.TeslimAdresi.Mahalle).NotEmpty().WithMessage("Geçerli bir mahalle giriniz!");
		RuleFor(p => p.TeslimAdresi.FullAddress).NotEmpty().WithMessage("Geçerli bir tam adres giriniz!");
		RuleFor(p => p.KargoInformation.KargoTipiValue)
			.GreaterThanOrEqualTo(0).WithMessage("Geçerli bir kargo tipi seçin!")
			.LessThan(KargoTipiEnum.List.Count()).WithMessage("Geçerli bir kargo tipi seçiniz!");

	}
}

internal sealed class KargoCreateCommandHandler(
    IKargoRespository kargoRepository,
	IUnitOfWork unitOfWork
	) : IRequestHandler<KargoCreateCommand, Result<string>>
{
	public async Task<Result<string>> Handle(KargoCreateCommand request, CancellationToken cancellationToken)
	{
		Kargo kargo = request.Adapt<Kargo>();
		KargoInformation kargoInformation = new(
			KargoTipiEnum.FromValue(request.KargoInformation.KargoTipiValue), request.KargoInformation.Agirlik);
		kargo.KargoInformation = kargoInformation;
		kargo.KargoDurum = KargoDurumEnum.Bekliyor;
		kargo.Alici = request.Alici;
		kargo.Gonderen = request.Gonderen;
		kargoRepository.Add(kargo);


		await unitOfWork.SaveChangesAsync(cancellationToken);


		// to do : burada mail ya da sms gönderme işlemleri yapılacak
		//to do : ileride notification içinde domain event kullanabiliriz.

		return "Kargo başarıyla kaydedildi!";
	}
}
