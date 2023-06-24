namespace CoreUtilities.Abstraction.Misc;

using System.Collections.Specialized;

public interface ISimpleClipboardAccessor
{
   #region Public Methods and Operators

   /// <summary>Clears any data from the system Clipboard.</summary>
   void Clear();

   /// <summary>Queries the Clipboard for the presence of data in the <see cref="F:System.Windows.DataFormats.WaveAudio"/> data format.</summary>
   /// <returns>
   ///    <see langword="true"/> if the Clipboard contains data in the <see cref="F:System.Windows.DataFormats.WaveAudio"/> data format; otherwise,
   ///    <see langword="false"/>.
   /// </returns>
   bool ContainsAudio();

   /// <summary>Queries the Clipboard for the presence of data in a specified data format.</summary>
   /// <param name="format">The format of the data to look for. See <see cref="T:System.Windows.DataFormats"/> for predefined formats.</param>
   /// <exception cref="T:System.ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
   /// <returns><see langword="true"/> if data in the specified format is available on the Clipboard; otherwise, <see langword="false"/>.</returns>
   bool ContainsData(string format);

   /// <summary>Queries the Clipboard for the presence of data in the <see cref="F:System.Windows.DataFormats.FileDrop"/> data format.</summary>
   /// <returns>
   ///    <see langword="true"/> if the Clipboard contains data in the <see cref="F:System.Windows.DataFormats.FileDrop"/> data format; otherwise,
   ///    <see langword="false"/>.
   /// </returns>
   bool ContainsFileDropList();

   /// <summary>Queries the Clipboard for the presence of data in the <see cref="F:System.Windows.DataFormats.Bitmap"/> data format.</summary>
   /// <returns>
   ///    <see langword="true"/> if the Clipboard contains data in the <see cref="F:System.Windows.DataFormats.Bitmap"/> data format; otherwise,
   ///    <see langword="false"/>.
   /// </returns>
   bool ContainsImage();

   /// <summary>Queries the Clipboard for the presence of data in the <see cref="F:System.Windows.DataFormats.UnicodeText"/> format.</summary>
   /// <returns>
   ///    <see langword="true"/> if the Clipboard contains data in the <see cref="F:System.Windows.DataFormats.UnicodeText"/> data format; otherwise,
   ///    <see langword="false"/>.
   /// </returns>
   bool ContainsText();

   /// <summary>
   ///    Permanently adds the data that is on the <see cref="T:System.Windows.Clipboard"/> so that it is available after the data's original
   ///    application closes.
   /// </summary>
   void Flush();

   /// <summary>Retrieves data in a specified format from the Clipboard.</summary>
   /// <param name="format">
   ///    A string that specifies the format of the data to retrieve. For a set of predefined data formats, see the
   ///    <see cref="T:System.Windows.DataFormats"/> class.
   /// </param>
   /// <exception cref="T:System.ArgumentNullException"><paramref name="format"/> is <see langword="null"/>.</exception>
   /// <returns>An object that contains the data in the specified format, or <see langword="null"/> if the data is unavailable in the specified format.</returns>
   object GetData(string format);

   /// <summary>Returns a string collection that contains a list of dropped files available on the Clipboard.</summary>
   /// <returns>
   ///    A collection of strings, where each string specifies the name of a file in the list of dropped files on the Clipboard, or
   ///    <see langword="null"/> if the data is unavailable in this format.
   /// </returns>
   StringCollection GetFileDropList();

   /// <summary>Returns a string containing the <see cref="F:System.Windows.DataFormats.UnicodeText"/> data on the Clipboard.</summary>
   /// <returns>
   ///    A string containing the <see cref="F:System.Windows.DataFormats.UnicodeText"/> data , or an empty string if no
   ///    <see cref="F:System.Windows.DataFormats.UnicodeText"/> data is available on the Clipboard.
   /// </returns>
   string GetText();

   /// <summary>Stores the specified data on the Clipboard in the specified format.</summary>
   /// <param name="format">
   ///    A string that specifies the format to use to store the data. See the <see cref="T:System.Windows.DataFormats"/> class for a set
   ///    of predefined data formats.
   /// </param>
   /// <param name="data">An object representing the data to store on the Clipboard.</param>
   void SetData(string format, object data);

   /// <summary>Places a specified non-persistent data object on the system Clipboard.</summary>
   /// <param name="data">A data object (an object that implements <see cref="T:System.Windows.IDataObject"/>) to place on the system Clipboard.</param>
   /// <exception cref="T:System.ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
   /// <exception cref="T:System.Runtime.InteropServices.ExternalException">
   ///    An error occurred when accessing the Clipboard. The exception details will
   ///    include an <see langword="HResult"/> that identifies the specific error; see <see cref="P:System.Runtime.InteropServices.ErrorWrapper.ErrorCode"/>
   ///    .
   /// </exception>
   void SetDataObject(object data);

   /// <summary>
   ///    Places a specified data object on the system Clipboard and accepts a Boolean parameter that indicates whether the data object should be left
   ///    on the Clipboard when the application exits.
   /// </summary>
   /// <param name="data">A data object (an object that implements <see cref="T:System.Windows.IDataObject"/>) to place on the system Clipboard.</param>
   /// <param name="copy">
   ///    <see langword="true"/> to leave the data on the system Clipboard when the application exits; <see langword="false"/> to clear the
   ///    data from the system Clipboard when the application exits.
   /// </param>
   /// <exception cref="T:System.ArgumentNullException"><paramref name="data"/> is <see langword="null"/>.</exception>
   /// <exception cref="T:System.Runtime.InteropServices.ExternalException">
   ///    An error occurred when accessing the Clipboard.  The exception details will
   ///    include an <see langword="HResult"/> that identifies the specific error; see <see cref="P:System.Runtime.InteropServices.ErrorWrapper.ErrorCode"/>
   ///    .
   /// </exception>
   void SetDataObject(object data, bool copy);

   /// <summary>Stores <see cref="F:System.Windows.DataFormats.UnicodeText"/> data on the Clipboard.</summary>
   /// <param name="text">A string that contains the <see cref="F:System.Windows.DataFormats.UnicodeText"/> data to store on the Clipboard.</param>
   /// <exception cref="T:System.ArgumentNullException"><paramref name="text"/> is <see langword="null"/>.</exception>
   void SetText(string text);

   #endregion
}