namespace KargoTakip.Server.Domain.Kargolarim;

public sealed record Address(	//Value Object
	string City,
	string Town,
	string Mahalle,
	string Street,
	string FullAddress
	 );
