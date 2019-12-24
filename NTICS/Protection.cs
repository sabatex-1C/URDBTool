using System;
using System.Collections.Generic;
using System.Text;

namespace NTICS
{
    public struct TSecretKey
    {
        string EDRPOU;
        int ID_FIZ;
        string COMPANY_NAME;
        int LICENZE;
        string SERIAL;
    }

    
    
    public class Protection
    {
        static string FChar = "WERTYUPASDFGHJKLZXCVBNM123456789";

        public static uint[] ByteArrayToUInt(byte[] Array)
        {
            int size = Array.Length / 4;
            if (Array.Length % 4 > 0) size++;
            uint[] result = new uint[size];
            int j = 0;
            int i = 0;
            while (i < Array.Length)
            {
                result[j] = result[j] | ((uint)Array[i] << (i % 4) * 8);
                i++;           
                if (((i % 4) == 0) && (i != 0)) j++;
            }
            return result;
        }

        public static byte[] UIntArrayToByte(uint[] Array)
        {
            int size = Array.Length * 4;
            byte[] result = new byte[size];
            int j = 0;
            foreach (uint value in Array)
            {
                result[j]   = (byte)(value);
                result[j+1] = (byte)(value >> 8);
                result[j + 2] = (byte)(value >> 16);
                result[j + 3] = (byte)(value >> 24);
                j = j + 4;
            }
            return result;
        }


        // Преобразуем целое число в строку символов для ввода используя CRC
        public static string IntToString(uint value)
        {
            string result = "";
            for (int i = 0; i < 6; i++)
            {
                result = result + FChar[(int)((value >> (i * 5)) & (uint)0x1f)];
            }
            // добавим CRC
            return result + FChar[(int)(((value >> (30)) & (uint)0x1f) | (crc3(value) << 2))];
        }

        // Преобразование строки символов в число с проверкой CRC
        public static uint StringToUInt(string Value)
        {
            if ((Value.Length < 7) || (Value.Length % 7 != 0)) throw new Exception("Ошибка преобразования строки");
            uint Result = 0;
            for (int i = 0; i < 6; i++)
            {
                Result = Result | ((uint)FChar.IndexOf(Value[i]) << (i*5));
            }
            // отделим CRC
            Result = Result | (((uint)FChar.IndexOf(Value[6]) << 30) & (uint)0xC0000000);
            // проверим CRC
            if (crc3(Result) != (((uint)FChar.IndexOf(Value[6]) >> 2) & (uint)0x00000007))
                throw new Exception("ошибка CRC");
            return Result;
        }



        // На каждое двойное слово отвотится 7 символов
        // слова разделяются знаком -
        public static string ArrayToString(uint[]  Value)
        {
            string s = "";
            for (int i = 0; i < Value.Length; i++)
            {
                if (i != 0) {s=s + "-";}
                s = s + IntToString(Value[i]);
            }
            return s;
        }


        // Количество символов должно быть кратно 7 (не учитывая разделителей)
        public static uint[] StringToArray(string Value)
        {
            // удаляем разделители '-'
            string s="";
            foreach (char c in Value)
            {
                if (c != '-') {s = s + c;}
            }
            if ((s.Length < 7) || (s.Length % 7 != 0)) throw new Exception("");
            uint[] Result = new uint[s.Length / 7];
            for (int i=0; i <  Math.Min(Result.Length, s.Length / 7); i++)
            {
                Result[i] = StringToUInt(s.Substring(i*7,7));
            }
            return Result;
        }



        // Серийный номер продажи
        public static string PaySerialNamber()
        {
            return config.LoadProperty("SERIAL", "SERIAL_KEY", "");
        }
        // Строку в массив байт
        public static byte[] StringAsBytes(string str)
        {
            byte[] Result = new byte[str.Length];
            int i =0;
            foreach (char c in str)
            {
                Result[i] = (byte)c;
                i++;
            }
            return Result;
        }
        public static byte[] HardwareKey()
        {
            return (byte[])Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\MountedDevices", "\\DosDevices\\C:", null);
        }
        public static byte[] HashHardvare()
        {
            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1Managed();
            return sha.ComputeHash(HardwareKey());
        }
        public Protection()
        {
        }
        public static uint crc3(uint value)
        {
            return value % 7;
        }



    }
}

/*
Type
  TRegistrationInfo = class(TObject)
  private
    FD:TFGint;
    FN:TFGint;
    FKey:TSecretKey;
    function GetEDRPOU: string;
    function GetFirmName: string;
    function GetLicenzeCount: integer;
    function GetTFO: integer;
  public
    constructor Create(const dkey,nkey:string);reintroduce;
    property EDRPOU:string read GetEDRPOU;
    property TFO:integer read GetTFO;
    property LicenzeCount:integer read GetLicenzeCount;
    property FirmName:string read GetFirmName;
    procedure Registration(SerialKey:string);
    procedure Decript;
  end;



function DecryptKey(Key:string;D,N:TFGInt):TSecretKey;
function LoadKeyFromIni(D,N:TFGInt):TSecretKey;
function GetAuteficationKey(SerialKey:string):string;


var
  RegistrationInfo:TRegistrationInfo;



{ TRegistrationInfo }

constructor TRegistrationInfo.Create;
var
  INIFile:TIniFile;
begin
  inherited Create();
  INIFile:=TIniFile.Create(ConfigStorage.StorageName);
  Base10StringToFGInt(dkey,FD);
  Base10StringToFGInt(nkey,FN);
  FKey:=DecryptKey(INIFile.ReadString('OPTIONS','SERIAL',''),FD,FN);
  INIFile.Free;
end;

procedure TRegistrationInfo.Decript;
var
  INIFile:TIniFile;
begin
  INIFile:=TIniFile.Create(ConfigStorage.StorageName);
  FKey:=DecryptKey(INIFile.ReadString('OPTIONS','SERIAL',''),FD,FN);
  INIFile.Free;
end;

function TRegistrationInfo.GetEDRPOU: string;
begin
  Result:=FKey.EDRPOU;
end;

function TRegistrationInfo.GetFirmName: string;
begin
  Result:=FKey.COMPANY_NAME;
end;

function TRegistrationInfo.GetLicenzeCount: integer;
begin
  Result:=FKey.LICENZE;
end;

function TRegistrationInfo.GetTFO: integer;
begin
  Result:=Fkey.ID_FIZ;
end;


procedure TRegistrationInfo.Registration(SerialKey: string);
var
  INIFile:TIniFile;
begin
  INIFile:=TIniFile.Create(ConfigStorage.StorageName);
  INIFile.WriteString('OPTIONS','SERIAL',SerialKey);
  FKey:=DecryptKey(SerialKey,FD,FN);
  INIFile.Free;
end;

function RSADecryptBase10(s:string;E,N:TFGInt):string;
var
  Null:TFGInt;
begin
  Base10StringToFGInt(s,Null);
  FGIntToBase256String(Null,s);
  FGIntDestroy(Null);
  RSADecrypt(s,E,N,Null,Null,Null,Null,Result);
  FGIntDestroy(e);
  FGIntDestroy(n);
end;

function LoadKeyFromIni(D,N:TFGInt):TSecretKey;
var
  s:string;
begin
  s:=ConfigStorage.LoadValue('Registration',Unregistred);
  Result:=DecryptKey(s,D,N);
end;

function DecryptKey(Key:string;D,N:TFGInt):TSecretKey;
  function TestEnd(s:string):boolean;
  begin
    Result:=(pos(';',s) = 0);
  end;

var
  s:string;
begin
  // значения по умолчанию
  Result.EDRPOU:=Unregistred;
  Result.COMPANY_NAME:='Demo';
  Result.ID_FIZ:=0;
  Result.LICENZE:=0;
  Result.SERIAL:=Unregistred;

  // Розшифровка ключа
  if Key = '' then Exit;
  s:=RSADecryptBase10(Key,D,N);
  if TestEnd(s) then Exit;
  // проверка версии ключа
  if copy(s,1,pos(';',s)-1) <> VersionKey then Exit;
  Delete(s,1,pos(';',s));
  if TestEnd(s) then Exit;
  if copy(s,1,pos(';',s)-1) <> Copyrigth then Exit;
  Delete(s,1,pos(';',s));
  if TestEnd(s) then Exit;
  // загрузка значений
  Result.EDRPOU:=copy(s,1,pos(';',s)-1);
  Delete(s,1,pos(';',s));
  if TestEnd(s) then Exit;
  Result.COMPANY_NAME:=copy(s,1,pos(';',s)-1);
  Delete(s,1,pos(';',s));
  if TestEnd(s) then Exit;
  Result.ID_FIZ:=StrToInt(copy(s,1,pos(';',s)-1));
  Delete(s,1,pos(';',s));
  if TestEnd(s) then Exit;
  Result.LICENZE:=StrToInt(copy(s,1,pos(';',s)-1));
  Delete(s,1,pos(';',s));
  if TestEnd(s) then Exit;
  Result.SERIAL:=copy(s,1,pos(';',s)-1);
end;

function GetAuteficationKey(SerialKey:string):string;
var
  s:string;
begin
  s:=GetHardwareNumber+';'+SerialKey;

end;

*/