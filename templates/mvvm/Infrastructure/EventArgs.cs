using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace MVVM.Infrastructure
{
    /// <summary>Аргумент события с типизированным параметром</summary>
    [DebuggerStepThrough]
    public class EventArgs<TArgument> : EventArgs
    {
        /// <summary>Параметр аргумента</summary>
        public TArgument Argument { get; set; }

        /// <summary>Инициализация нового экземпляра <see cref="T:System.EventArgs`1" /></summary>
        public EventArgs() { }

        /// <summary>Инициализация нового экземпляра <see cref="T:System.EventArgs`1" /></summary>
        /// <param name="Argument">Параметр аргумента</param>
        public EventArgs(TArgument Argument) => this.Argument = Argument;

        /// <summary>Строковое представление аргумента события</summary>
        public override string ToString() => Argument.ToString();

        /// <summary>Оператор неявного преобразования аргумента события к типу содержащегося в нём значения</summary>
        /// <param name="Args">Аргумент события</param>
        /// <returns>Хранимый объект</returns>
        public static implicit operator TArgument([NotNull] EventArgs<TArgument> Args) => Args.Argument;

        /// <summary>Оператор неявного преобразования типа хранимого значения в обёртку из аргумента события, содержащего это значение</summary>
        /// <param name="Argument">Объект аргумента события</param>
        /// <returns>Аргумент события</returns>
        public static implicit operator EventArgs<TArgument>(TArgument Argument) => new(Argument);
    }
}
