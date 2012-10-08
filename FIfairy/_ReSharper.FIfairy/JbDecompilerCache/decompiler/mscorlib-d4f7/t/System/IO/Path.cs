// Type: System.IO.Path
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
  [ComVisible(true)]
  public static class Path
  {
    public static readonly char DirectorySeparatorChar = '\\';
    public static readonly char AltDirectorySeparatorChar = '/';
    public static readonly char VolumeSeparatorChar = ':';
    [Obsolete("Please use GetInvalidPathChars or GetInvalidFileNameChars instead.")]
    public static readonly char[] InvalidPathChars = new char[36]
    {
      '"',
      '<',
      '>',
      '|',
      char.MinValue,
      '\x0001',
      '\x0002',
      '\x0003',
      '\x0004',
      '\x0005',
      '\x0006',
      '\a',
      '\b',
      '\t',
      '\n',
      '\v',
      '\f',
      '\r',
      '\x000E',
      '\x000F',
      '\x0010',
      '\x0011',
      '\x0012',
      '\x0013',
      '\x0014',
      '\x0015',
      '\x0016',
      '\x0017',
      '\x0018',
      '\x0019',
      '\x001A',
      '\x001B',
      '\x001C',
      '\x001D',
      '\x001E',
      '\x001F'
    };
    internal static readonly char[] TrimEndChars = new char[8]
    {
      '\t',
      '\n',
      '\v',
      '\f',
      '\r',
      ' ',
      '\x0085',
      ' '
    };
    private static readonly char[] RealInvalidPathChars = new char[36]
    {
      '"',
      '<',
      '>',
      '|',
      char.MinValue,
      '\x0001',
      '\x0002',
      '\x0003',
      '\x0004',
      '\x0005',
      '\x0006',
      '\a',
      '\b',
      '\t',
      '\n',
      '\v',
      '\f',
      '\r',
      '\x000E',
      '\x000F',
      '\x0010',
      '\x0011',
      '\x0012',
      '\x0013',
      '\x0014',
      '\x0015',
      '\x0016',
      '\x0017',
      '\x0018',
      '\x0019',
      '\x001A',
      '\x001B',
      '\x001C',
      '\x001D',
      '\x001E',
      '\x001F'
    };
    private static readonly char[] InvalidFileNameChars = new char[41]
    {
      '"',
      '<',
      '>',
      '|',
      char.MinValue,
      '\x0001',
      '\x0002',
      '\x0003',
      '\x0004',
      '\x0005',
      '\x0006',
      '\a',
      '\b',
      '\t',
      '\n',
      '\v',
      '\f',
      '\r',
      '\x000E',
      '\x000F',
      '\x0010',
      '\x0011',
      '\x0012',
      '\x0013',
      '\x0014',
      '\x0015',
      '\x0016',
      '\x0017',
      '\x0018',
      '\x0019',
      '\x001A',
      '\x001B',
      '\x001C',
      '\x001D',
      '\x001E',
      '\x001F',
      ':',
      '*',
      '?',
      '\\',
      '/'
    };
    public static readonly char PathSeparator = ';';
    internal static readonly int MaxPath = 260;
    private static readonly int MaxDirectoryLength = (int) byte.MaxValue;
    internal static readonly int MaxLongPath = 32000;
    private static readonly string Prefix = "\\\\?\\";
    private static char[] s_Base32Char = new char[32]
    {
      'a',
      'b',
      'c',
      'd',
      'e',
      'f',
      'g',
      'h',
      'i',
      'j',
      'k',
      'l',
      'm',
      'n',
      'o',
      'p',
      'q',
      'r',
      's',
      't',
      'u',
      'v',
      'w',
      'x',
      'y',
      'z',
      '0',
      '1',
      '2',
      '3',
      '4',
      '5'
    };
    internal const int MAX_PATH = 260;
    internal const int MAX_DIRECTORY_PATH = 248;

    static Path()
    {
    }

    public static string ChangeExtension(string path, string extension)
    {
      if (path == null)
        return (string) null;
      Path.CheckInvalidPathChars(path);
      string str = path;
      int length = path.Length;
      while (--length >= 0)
      {
        char ch = path[length];
        if ((int) ch == 46)
        {
          str = path.Substring(0, length);
          break;
        }
        else if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
          break;
      }
      if (extension != null && path.Length != 0)
      {
        if (extension.Length == 0 || (int) extension[0] != 46)
          str = str + ".";
        str = str + extension;
      }
      return str;
    }

    [SecuritySafeCritical]
    public static string GetDirectoryName(string path)
    {
      if (path != null)
      {
        Path.CheckInvalidPathChars(path);
        path = Path.NormalizePath(path, false);
        int rootLength = Path.GetRootLength(path);
        if (path.Length > rootLength)
        {
          int length = path.Length;
          if (length == rootLength)
            return (string) null;
          do
            ;
          while (length > rootLength && (int) path[--length] != (int) Path.DirectorySeparatorChar && (int) path[length] != (int) Path.AltDirectorySeparatorChar);
          return path.Substring(0, length);
        }
      }
      return (string) null;
    }

    internal static int GetRootLength(string path)
    {
      Path.CheckInvalidPathChars(path);
      int index = 0;
      int length = path.Length;
      if (length >= 1 && Path.IsDirectorySeparator(path[0]))
      {
        index = 1;
        if (length >= 2 && Path.IsDirectorySeparator(path[1]))
        {
          index = 2;
          int num = 2;
          while (index < length && ((int) path[index] != (int) Path.DirectorySeparatorChar && (int) path[index] != (int) Path.AltDirectorySeparatorChar || --num > 0))
            ++index;
        }
      }
      else if (length >= 2 && (int) path[1] == (int) Path.VolumeSeparatorChar)
      {
        index = 2;
        if (length >= 3 && Path.IsDirectorySeparator(path[2]))
          ++index;
      }
      return index;
    }

    internal static bool IsDirectorySeparator(char c)
    {
      if ((int) c != (int) Path.DirectorySeparatorChar)
        return (int) c == (int) Path.AltDirectorySeparatorChar;
      else
        return true;
    }

    public static char[] GetInvalidPathChars()
    {
      return (char[]) Path.RealInvalidPathChars.Clone();
    }

    public static char[] GetInvalidFileNameChars()
    {
      return (char[]) Path.InvalidFileNameChars.Clone();
    }

    public static string GetExtension(string path)
    {
      if (path == null)
        return (string) null;
      Path.CheckInvalidPathChars(path);
      int length = path.Length;
      int startIndex = length;
      while (--startIndex >= 0)
      {
        char ch = path[startIndex];
        if ((int) ch == 46)
        {
          if (startIndex != length - 1)
            return path.Substring(startIndex, length - startIndex);
          else
            return string.Empty;
        }
        else if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
          break;
      }
      return string.Empty;
    }

    [SecuritySafeCritical]
    public static string GetFullPath(string path)
    {
      string fullPathInternal = Path.GetFullPathInternal(path);
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, new string[1]
      {
        fullPathInternal
      }, 0 != 0, 0 != 0).Demand();
      return fullPathInternal;
    }

    internal static string GetFullPathInternal(string path)
    {
      if (path == null)
        throw new ArgumentNullException("path");
      else
        return Path.NormalizePath(path, true);
    }

    [SecuritySafeCritical]
    internal static string NormalizePath(string path, bool fullCheck)
    {
      return Path.NormalizePath(path, fullCheck, Path.MaxPath);
    }

    [SecurityCritical]
    internal static unsafe string NormalizePath(string path, bool fullCheck, int maxPathLength)
    {
      if (fullCheck)
      {
        path = path.TrimEnd(Path.TrimEndChars);
        Path.CheckInvalidPathChars(path);
      }
      int index1 = 0;
      PathHelper pathHelper;
      if (path.Length <= Path.MaxPath)
      {
        char* charArrayPtr = stackalloc char[Path.MaxPath];
        pathHelper = new PathHelper(charArrayPtr, Path.MaxPath);
      }
      else
        pathHelper = new PathHelper(path.Length + Path.MaxPath, maxPathLength);
      uint num1 = 0U;
      uint num2 = 0U;
      bool flag1 = false;
      uint num3 = 0U;
      int num4 = -1;
      bool flag2 = false;
      bool flag3 = true;
      bool flag4 = false;
      int num5 = 0;
      if (path.Length > 0 && ((int) path[0] == (int) Path.DirectorySeparatorChar || (int) path[0] == (int) Path.AltDirectorySeparatorChar))
      {
        pathHelper.Append('\\');
        ++index1;
        num4 = 0;
      }
      for (; index1 < path.Length; ++index1)
      {
        char ch1 = path[index1];
        if ((int) ch1 == (int) Path.DirectorySeparatorChar || (int) ch1 == (int) Path.AltDirectorySeparatorChar)
        {
          if ((int) num3 == 0)
          {
            if (num2 > 0U)
            {
              int index2 = num4 + 1;
              if ((int) path[index2] != 46)
                throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
              if (num2 >= 2U)
              {
                if (flag2 && num2 > 2U)
                  throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
                if ((int) path[index2 + 1] == 46)
                {
                  for (int index3 = index2 + 2; (long) index3 < (long) index2 + (long) num2; ++index3)
                  {
                    if ((int) path[index3] != 46)
                      throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
                  }
                  num2 = 2U;
                }
                else
                {
                  if (num2 > 1U)
                    throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
                  num2 = 1U;
                }
              }
              if ((int) num2 == 2)
                pathHelper.Append('.');
              pathHelper.Append('.');
              flag1 = false;
            }
            if (num1 > 0U && flag3 && index1 + 1 < path.Length && ((int) path[index1 + 1] == (int) Path.DirectorySeparatorChar || (int) path[index1 + 1] == (int) Path.AltDirectorySeparatorChar))
              pathHelper.Append(Path.DirectorySeparatorChar);
          }
          num2 = 0U;
          num1 = 0U;
          if (!flag1)
          {
            flag1 = true;
            pathHelper.Append(Path.DirectorySeparatorChar);
          }
          num3 = 0U;
          num4 = index1;
          flag2 = false;
          flag3 = false;
          if (flag4)
          {
            pathHelper.TryExpandShortFileName();
            flag4 = false;
          }
          int num6 = pathHelper.Length - 1;
          if (num6 - num5 > Path.MaxDirectoryLength)
            throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
          num5 = num6;
        }
        else if ((int) ch1 == 46)
          ++num2;
        else if ((int) ch1 == 32)
        {
          ++num1;
        }
        else
        {
          if ((int) ch1 == 126)
            flag4 = true;
          flag1 = false;
          if (flag3 && (int) ch1 == (int) Path.VolumeSeparatorChar)
          {
            char ch2 = index1 > 0 ? path[index1 - 1] : ' ';
            if ((int) num2 != 0 || num3 < 1U || (int) ch2 == 32)
              throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
            flag2 = true;
            if (num3 > 1U)
            {
              int index2 = 0;
              while (index2 < pathHelper.Length && (int) pathHelper[index2] == 32)
                ++index2;
              if ((long) num3 - (long) index2 == 1L)
              {
                pathHelper.Length = 0;
                pathHelper.Append(ch2);
              }
            }
            num3 = 0U;
          }
          else
            num3 += 1U + num2 + num1;
          if (num2 > 0U || num1 > 0U)
          {
            int num6 = num4 >= 0 ? index1 - num4 - 1 : index1;
            if (num6 > 0)
            {
              for (int index2 = 0; index2 < num6; ++index2)
                pathHelper.Append(path[num4 + 1 + index2]);
            }
            num2 = 0U;
            num1 = 0U;
          }
          pathHelper.Append(ch1);
          num4 = index1;
        }
      }
      if (pathHelper.Length - 1 - num5 > Path.MaxDirectoryLength)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      if ((int) num3 == 0 && num2 > 0U)
      {
        int index2 = num4 + 1;
        if ((int) path[index2] != 46)
          throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
        if (num2 >= 2U)
        {
          if (flag2 && num2 > 2U)
            throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
          if ((int) path[index2 + 1] == 46)
          {
            for (int index3 = index2 + 2; (long) index3 < (long) index2 + (long) num2; ++index3)
            {
              if ((int) path[index3] != 46)
                throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
            }
            num2 = 2U;
          }
          else
          {
            if (num2 > 1U)
              throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
            num2 = 1U;
          }
        }
        if ((int) num2 == 2)
          pathHelper.Append('.');
        pathHelper.Append('.');
      }
      if (pathHelper.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegal"));
      if (fullCheck && (pathHelper.OrdinalStartsWith("http:", false) || pathHelper.OrdinalStartsWith("file:", false)))
        throw new ArgumentException(Environment.GetResourceString("Argument_PathUriFormatNotSupported"));
      if (flag4)
        pathHelper.TryExpandShortFileName();
      int num7 = 1;
      if (fullCheck)
      {
        num7 = pathHelper.GetFullPathName();
        bool flag5 = false;
        for (int index2 = 0; index2 < pathHelper.Length && !flag5; ++index2)
        {
          if ((int) pathHelper[index2] == 126)
            flag5 = true;
        }
        if (flag5 && !pathHelper.TryExpandShortFileName())
        {
          int lastSlash = -1;
          for (int index2 = pathHelper.Length - 1; index2 >= 0; --index2)
          {
            if ((int) pathHelper[index2] == (int) Path.DirectorySeparatorChar)
            {
              lastSlash = index2;
              break;
            }
          }
          if (lastSlash >= 0)
          {
            if (pathHelper.Length >= maxPathLength)
              throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
            int lenSavedName = pathHelper.Length - lastSlash - 1;
            pathHelper.Fixup(lenSavedName, lastSlash);
          }
        }
      }
      if (num7 != 0 && pathHelper.Length > 1 && ((int) pathHelper[0] == 92 && (int) pathHelper[1] == 92))
      {
        int index2;
        for (index2 = 2; index2 < num7; ++index2)
        {
          if ((int) pathHelper[index2] == 92)
          {
            ++index2;
            break;
          }
        }
        if (index2 == num7)
          throw new ArgumentException(Environment.GetResourceString("Arg_PathIllegalUNC"));
        if (pathHelper.OrdinalStartsWith("\\\\?\\globalroot", true))
          throw new ArgumentException(Environment.GetResourceString("Arg_PathGlobalRoot"));
      }
      if (pathHelper.Length >= maxPathLength)
        throw new PathTooLongException(Environment.GetResourceString("IO.PathTooLong"));
      if (num7 == 0)
      {
        int errorCode = Marshal.GetLastWin32Error();
        if (errorCode == 0)
          errorCode = 161;
        __Error.WinIOError(errorCode, path);
        return (string) null;
      }
      else
      {
        string a = pathHelper.ToString();
        if (string.Equals(a, path, StringComparison.Ordinal))
          a = path;
        return a;
      }
    }

    internal static bool HasLongPathPrefix(string path)
    {
      return path.StartsWith(Path.Prefix, StringComparison.Ordinal);
    }

    internal static string AddLongPathPrefix(string path)
    {
      if (path.StartsWith(Path.Prefix, StringComparison.Ordinal))
        return path;
      else
        return Path.Prefix + path;
    }

    internal static string RemoveLongPathPrefix(string path)
    {
      if (!path.StartsWith(Path.Prefix, StringComparison.Ordinal))
        return path;
      else
        return path.Substring(4);
    }

    internal static StringBuilder RemoveLongPathPrefix(StringBuilder path)
    {
      if (!((object) path).ToString().StartsWith(Path.Prefix, StringComparison.Ordinal))
        return path;
      else
        return path.Remove(0, 4);
    }

    public static string GetFileName(string path)
    {
      if (path != null)
      {
        Path.CheckInvalidPathChars(path);
        int length = path.Length;
        int index = length;
        while (--index >= 0)
        {
          char ch = path[index];
          if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
            return path.Substring(index + 1, length - index - 1);
        }
      }
      return path;
    }

    public static string GetFileNameWithoutExtension(string path)
    {
      path = Path.GetFileName(path);
      if (path == null)
        return (string) null;
      int length;
      if ((length = path.LastIndexOf('.')) == -1)
        return path;
      else
        return path.Substring(0, length);
    }

    [SecuritySafeCritical]
    public static string GetPathRoot(string path)
    {
      if (path == null)
        return (string) null;
      path = Path.NormalizePath(path, false);
      return path.Substring(0, Path.GetRootLength(path));
    }

    [SecuritySafeCritical]
    public static string GetTempPath()
    {
      new EnvironmentPermission(PermissionState.Unrestricted).Demand();
      StringBuilder buffer = new StringBuilder(260);
      uint tempPath = Win32Native.GetTempPath(260, buffer);
      string path = ((object) buffer).ToString();
      if ((int) tempPath == 0)
        __Error.WinIOError();
      return Path.GetFullPathInternal(path);
    }

    internal static bool IsRelative(string path)
    {
      if (path.Length >= 3 && (int) path[1] == (int) Path.VolumeSeparatorChar && (int) path[2] == (int) Path.DirectorySeparatorChar && ((int) path[0] >= 97 && (int) path[0] <= 122 || (int) path[0] >= 65 && (int) path[0] <= 90) || path.Length >= 2 && (int) path[0] == 92 && (int) path[1] == 92)
        return false;
      else
        return true;
    }

    public static string GetRandomFileName()
    {
      byte[] numArray = new byte[10];
      RNGCryptoServiceProvider cryptoServiceProvider = (RNGCryptoServiceProvider) null;
      try
      {
        cryptoServiceProvider = new RNGCryptoServiceProvider();
        cryptoServiceProvider.GetBytes(numArray);
        char[] chArray = Path.ToBase32StringSuitableForDirName(numArray).ToCharArray();
        chArray[8] = '.';
        return new string(chArray, 0, 12);
      }
      finally
      {
        if (cryptoServiceProvider != null)
          cryptoServiceProvider.Dispose();
      }
    }

    [SecuritySafeCritical]
    public static string GetTempFileName()
    {
      string tempPath = Path.GetTempPath();
      new FileIOPermission(FileIOPermissionAccess.Write, tempPath).Demand();
      StringBuilder tmpFileName = new StringBuilder(260);
      if ((int) Win32Native.GetTempFileName(tempPath, "tmp", 0U, tmpFileName) == 0)
        __Error.WinIOError();
      return ((object) tmpFileName).ToString();
    }

    public static bool HasExtension(string path)
    {
      if (path != null)
      {
        Path.CheckInvalidPathChars(path);
        int length = path.Length;
        while (--length >= 0)
        {
          char ch = path[length];
          if ((int) ch == 46)
          {
            if (length != path.Length - 1)
              return true;
            else
              return false;
          }
          else if ((int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar || (int) ch == (int) Path.VolumeSeparatorChar)
            break;
        }
      }
      return false;
    }

    public static bool IsPathRooted(string path)
    {
      if (path != null)
      {
        Path.CheckInvalidPathChars(path);
        int length = path.Length;
        if (length >= 1 && ((int) path[0] == (int) Path.DirectorySeparatorChar || (int) path[0] == (int) Path.AltDirectorySeparatorChar) || length >= 2 && (int) path[1] == (int) Path.VolumeSeparatorChar)
          return true;
      }
      return false;
    }

    public static string Combine(string path1, string path2)
    {
      if (path1 == null || path2 == null)
        throw new ArgumentNullException(path1 == null ? "path1" : "path2");
      Path.CheckInvalidPathChars(path1);
      Path.CheckInvalidPathChars(path2);
      return Path.CombineNoChecks(path1, path2);
    }

    public static string Combine(string path1, string path2, string path3)
    {
      if (path1 == null || path2 == null || path3 == null)
        throw new ArgumentNullException(path1 == null ? "path1" : (path2 == null ? "path2" : "path3"));
      Path.CheckInvalidPathChars(path1);
      Path.CheckInvalidPathChars(path2);
      Path.CheckInvalidPathChars(path3);
      return Path.CombineNoChecks(Path.CombineNoChecks(path1, path2), path3);
    }

    public static string Combine(string path1, string path2, string path3, string path4)
    {
      if (path1 == null || path2 == null || (path3 == null || path4 == null))
        throw new ArgumentNullException(path1 == null ? "path1" : (path2 == null ? "path2" : (path3 == null ? "path3" : "path4")));
      Path.CheckInvalidPathChars(path1);
      Path.CheckInvalidPathChars(path2);
      Path.CheckInvalidPathChars(path3);
      Path.CheckInvalidPathChars(path4);
      return Path.CombineNoChecks(Path.CombineNoChecks(Path.CombineNoChecks(path1, path2), path3), path4);
    }

    public static string Combine(params string[] paths)
    {
      if (paths == null)
        throw new ArgumentNullException("paths");
      int capacity = 0;
      int num = 0;
      for (int index = 0; index < paths.Length; ++index)
      {
        if (paths[index] == null)
          throw new ArgumentNullException("paths");
        if (paths[index].Length != 0)
        {
          Path.CheckInvalidPathChars(paths[index]);
          if (Path.IsPathRooted(paths[index]))
          {
            num = index;
            capacity = paths[index].Length;
          }
          else
            capacity += paths[index].Length;
          char ch = paths[index][paths[index].Length - 1];
          if ((int) ch != (int) Path.DirectorySeparatorChar && (int) ch != (int) Path.AltDirectorySeparatorChar && (int) ch != (int) Path.VolumeSeparatorChar)
            ++capacity;
        }
      }
      StringBuilder stringBuilder = new StringBuilder(capacity);
      for (int index = num; index < paths.Length; ++index)
      {
        if (paths[index].Length != 0)
        {
          if (stringBuilder.Length == 0)
          {
            stringBuilder.Append(paths[index]);
          }
          else
          {
            char ch = stringBuilder[stringBuilder.Length - 1];
            if ((int) ch != (int) Path.DirectorySeparatorChar && (int) ch != (int) Path.AltDirectorySeparatorChar && (int) ch != (int) Path.VolumeSeparatorChar)
              stringBuilder.Append(Path.DirectorySeparatorChar);
            stringBuilder.Append(paths[index]);
          }
        }
      }
      return ((object) stringBuilder).ToString();
    }

    private static string CombineNoChecks(string path1, string path2)
    {
      if (path2.Length == 0)
        return path1;
      if (path1.Length == 0 || Path.IsPathRooted(path2))
        return path2;
      char ch = path1[path1.Length - 1];
      if ((int) ch != (int) Path.DirectorySeparatorChar && (int) ch != (int) Path.AltDirectorySeparatorChar && (int) ch != (int) Path.VolumeSeparatorChar)
        return path1 + (object) Path.DirectorySeparatorChar + path2;
      else
        return path1 + path2;
    }

    internal static string ToBase32StringSuitableForDirName(byte[] buff)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int length = buff.Length;
      int num1 = 0;
      do
      {
        byte num2 = num1 < length ? buff[num1++] : (byte) 0;
        byte num3 = num1 < length ? buff[num1++] : (byte) 0;
        byte num4 = num1 < length ? buff[num1++] : (byte) 0;
        byte num5 = num1 < length ? buff[num1++] : (byte) 0;
        byte num6 = num1 < length ? buff[num1++] : (byte) 0;
        stringBuilder.Append(Path.s_Base32Char[(int) num2 & 31]);
        stringBuilder.Append(Path.s_Base32Char[(int) num3 & 31]);
        stringBuilder.Append(Path.s_Base32Char[(int) num4 & 31]);
        stringBuilder.Append(Path.s_Base32Char[(int) num5 & 31]);
        stringBuilder.Append(Path.s_Base32Char[(int) num6 & 31]);
        stringBuilder.Append(Path.s_Base32Char[((int) num2 & 224) >> 5 | ((int) num5 & 96) >> 2]);
        stringBuilder.Append(Path.s_Base32Char[((int) num3 & 224) >> 5 | ((int) num6 & 96) >> 2]);
        byte num7 = (byte) ((uint) num4 >> 5);
        if (((int) num5 & 128) != 0)
          num7 |= (byte) 8;
        if (((int) num6 & 128) != 0)
          num7 |= (byte) 16;
        stringBuilder.Append(Path.s_Base32Char[(int) num7]);
      }
      while (num1 < length);
      return ((object) stringBuilder).ToString();
    }

    [SecuritySafeCritical]
    internal static void CheckSearchPattern(string searchPattern)
    {
      int num;
      for (; (num = searchPattern.IndexOf("..", StringComparison.Ordinal)) != -1; searchPattern = searchPattern.Substring(num + 2))
      {
        if (num + 2 == searchPattern.Length)
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidSearchPattern"));
        if ((int) searchPattern[num + 2] == (int) Path.DirectorySeparatorChar || (int) searchPattern[num + 2] == (int) Path.AltDirectorySeparatorChar)
          throw new ArgumentException(Environment.GetResourceString("Arg_InvalidSearchPattern"));
      }
    }

    internal static void CheckInvalidPathChars(string path)
    {
      for (int index = 0; index < path.Length; ++index)
      {
        int num = (int) path[index];
        switch (num)
        {
          case 34:
          case 60:
          case 62:
          case 124:
            throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
          default:
            if (num >= 32)
              continue;
            else
              goto case 34;
        }
      }
    }

    internal static string InternalCombine(string path1, string path2)
    {
      if (path1 == null || path2 == null)
        throw new ArgumentNullException(path1 == null ? "path1" : "path2");
      Path.CheckInvalidPathChars(path1);
      Path.CheckInvalidPathChars(path2);
      if (path2.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_PathEmpty"), "path2");
      if (Path.IsPathRooted(path2))
        throw new ArgumentException(Environment.GetResourceString("Arg_Path2IsRooted"), "path2");
      int length = path1.Length;
      if (length == 0)
        return path2;
      char ch = path1[length - 1];
      if ((int) ch != (int) Path.DirectorySeparatorChar && (int) ch != (int) Path.AltDirectorySeparatorChar && (int) ch != (int) Path.VolumeSeparatorChar)
        return path1 + (object) Path.DirectorySeparatorChar + path2;
      else
        return path1 + path2;
    }
  }
}
