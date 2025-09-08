using Ardalis.SmartEnum;

namespace KargoTakip.Server.Domain.Kargolarim;

public sealed class KargoDurumEnum : SmartEnum<KargoDurumEnum>
{
	public static KargoDurumEnum Bekliyor = new("Bekliyor...", 0);
	public static KargoDurumEnum AracaTeslimEdildi = new("Araca Teslim edildi!", 1);
	public static KargoDurumEnum YolaCikti = new("Yola Çıktı!", 2);
	public static KargoDurumEnum TeslimSubesineUlasti = new("Teslim şubasine ulaştı!", 3);
	public static KargoDurumEnum TeslimIcinYolaCikti = new("Teslim için yola çıktı!!", 4);
	public static KargoDurumEnum TeslimEdildi = new("Teslim edildi!", 5);
	public static KargoDurumEnum AdresteKimseBulunamadi = new("Adreste kimse bulunamadı.", 6);
	public static KargoDurumEnum IptalEdildi = new("Iptal Edildi. ", 7);

	public KargoDurumEnum(string name, int value) : base(name, value)
	{
	}
}
